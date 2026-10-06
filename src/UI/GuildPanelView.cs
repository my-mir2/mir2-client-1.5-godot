using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大热血传奇 1.5 官方行会面板 (TGuildDlg, 纯 C# 实现)
/// 严格对齐官方 Prguse_0180 (628x453), 包含主页三段公告、成员列表网格、编辑公告子窗 (Prguse_0204) 与编辑封号子窗 (Prguse_0181)
/// </summary>
public partial class GuildPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void HomeRequestedEventHandler();

    [Signal]
    public delegate void MemberListRequestedEventHandler();

    [Signal]
    public delegate void PanelClosedEventHandler();

    [Signal]
    public delegate void AddMemberRequestedEventHandler(string targetName);

    [Signal]
    public delegate void DelMemberRequestedEventHandler(string targetName);

    [Signal]
    public delegate void AllyRequestedEventHandler(string targetGuild);

    [Signal]
    public delegate void CancelAllyRequestedEventHandler(string targetGuild);

    [Signal]
    public delegate void WarRequestedEventHandler(string targetGuild);

    [Signal]
    public delegate void CancelWarRequestedEventHandler(string targetGuild);

    [Signal]
    public delegate void NoticeUpdatedEventHandler(string newNotice);

    [Signal]
    public delegate void RankUpdatedEventHandler(string memberName, string newRank);

    public const float PanelWidth = 628.0f;
    public const float PanelHeight = 453.0f;
    public const int PageSize = 13;

    private Panel? _panel;
    private Control? _homeView;
    private Control? _memberListView;
    private RichTextLabel? _noticeText;
    private VBoxContainer? _memberListContainer;
    private Label? _lblMemberCount;
    private Label? _lblGuildTitle;

    // 底栏主页与名单切页按钮及其贴图缓存
    private TextureButton? _btnHome;
    private TextureButton? _btnMembers;
    private Texture2D? _texHomeNormal;
    private Texture2D? _texHomePressed;
    private Texture2D? _texMembersNormal;
    private Texture2D? _texMembersPressed;

    // 二级管理子窗体
    private GuildEditNoticeDialog? _editNoticeDlg;
    private GuildEditRankDialog? _editRankDlg;
    private MessageBoxDialog? _messageBoxDialog;

    // 8 大会长管理特权按钮 (IDA sub_4930A0 [0x4930fa..0x4931fe])
    private readonly List<TextureButton> _adminButtons = new();
    private TextureButton? _btnAddMember;
    private TextureButton? _btnDelMember;
    private TextureButton? _btnEditNotice;
    private TextureButton? _btnEditRank;
    private TextureButton? _btnAlly;
    private TextureButton? _btnCancelAlly;
    private TextureButton? _btnWar;
    private TextureButton? _btnCancelWar;

    public bool IsLeader { get; private set; } = true;

    // 成员数据与当前选中项
    private readonly List<(string role, string name, string job, int level, bool online)> _members = new();
    private int _currentPage = 0;
    private (string role, string name, string job, int level, bool online)? _selectedMember = null;
    private string _currentRawNotice = "";

    public string WindowName => "GuildPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public GuildEditNoticeDialog? EditNoticeDlg => _editNoticeDlg;
    public GuildEditRankDialog? EditRankDlg => _editRankDlg;
    public MessageBoxDialog? MsgBox => _messageBoxDialog;
    public (string role, string name, string job, int level, bool online)? SelectedMember => _selectedMember;

    public IReadOnlyList<TextureButton> AdminButtons => _adminButtons;
    public TextureButton? BtnAddMember => _btnAddMember;
    public TextureButton? BtnDelMember => _btnDelMember;
    public TextureButton? BtnEditNotice => _btnEditNotice;
    public TextureButton? BtnEditRank => _btnEditRank;
    public TextureButton? BtnAlly => _btnAlly;
    public TextureButton? BtnCancelAlly => _btnCancelAlly;
    public TextureButton? BtnWar => _btnWar;
    public TextureButton? BtnCancelWar => _btnCancelWar;

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(PanelWidth, PanelHeight);
        Position = new Vector2(86, 73);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0180.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生行会关闭按钮 (Prguse_0064 @ 584, 6, IDA sub_481dd4 [0x485a6c..0x485a9f])
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Position = new Vector2(584, 6),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方原生行会名称金属凹槽 (Prguse_0180 物理槽位 @ (300, 10, 158, 18)，左侧 276~299 为徽章槽)
        _lblGuildTitle = new Label
        {
            Name = "LblGuildTitle",
            Position = new Vector2(302, 10),
            Size = new Vector2(154, 18),
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblGuildTitle.AddThemeFontSizeOverride("font_size", 11);
        _lblGuildTitle.AddThemeColorOverride("font_color", new Color(1.0f, 0.85f, 0.4f));
        _panel.AddChild(_lblGuildTitle);

        // 中央内容区容器 (高光边框内胆 Left=15, Top=35, Width=570, Height=368)
        SetupHomeView();
        SetupMemberListView();

        // 挂载官方底栏 11 大按钮与右侧翻页按钮
        SetupButtons();

        // 初始化二级子窗口
        _editNoticeDlg = new GuildEditNoticeDialog();
        _editNoticeDlg.NoticeSubmitted += (notice) =>
        {
            _currentRawNotice = notice;
            UpdateHomeNotice(notice);
            EmitSignal(SignalName.NoticeUpdated, notice);
        };
        AddChild(_editNoticeDlg);

        _editRankDlg = new GuildEditRankDialog();
        _editRankDlg.RankSubmitted += (name, rank) =>
        {
            EmitSignal(SignalName.RankUpdated, name, rank);
            // 本地临时同步更新显示
            for (int i = 0; i < _members.Count; i++)
            {
                if (_members[i].name == name)
                {
                    _members[i] = (rank, _members[i].name, _members[i].job, _members[i].level, _members[i].online);
                    break;
                }
            }
            RenderCurrentPageMembers();
        };
        AddChild(_editRankDlg);
    }

    private void SetupHomeView()
    {
        _homeView = new Control
        {
            Name = "GuildHomeView",
            Position = new Vector2(15, 35),
            Size = new Vector2(570, 368),
            MouseFilter = MouseFilterEnum.Pass
        };
        _panel?.AddChild(_homeView);

        _noticeText = new RichTextLabel
        {
            Name = "GuildNoticeText",
            Position = new Vector2(12, 12),
            Size = new Vector2(546, 268),
            BbcodeEnabled = true,
            Text = ""
        };
        _noticeText.AddThemeFontSizeOverride("normal_font_size", 11);
        _homeView.AddChild(_noticeText);

        // 官方 sub_493F58 (0x49401d): 灰色 0xC0C0C0 文本 @(18, 288), 取自窗体字段 +2484
        var lblHint = new Label
        {
            Name = "LblGuildHint",
            Position = new Vector2(18, 288),
            Text = "请点击 [名单]按钮以调出行会成员信息。"
        };
        lblHint.AddThemeFontSizeOverride("font_size", 12);
        lblHint.AddThemeColorOverride("font_color", new Color(0.753f, 0.753f, 0.753f));
        _homeView.AddChild(lblHint);
    }

    private void SetupMemberListView()
    {
        _memberListView = new Control
        {
            Name = "GuildMemberListView",
            Position = new Vector2(15, 35),
            Size = new Vector2(570, 368),
            MouseFilter = MouseFilterEnum.Pass,
            Visible = false
        };
        _panel?.AddChild(_memberListView);

        // 成员列表 5 列标准表头 (对齐官方 1.5 成员网格)
        var headerContainer = new Control
        {
            Position = new Vector2(12, 8),
            Size = new Vector2(550, 22)
        };
        _memberListView.AddChild(headerContainer);

        var colRole = new Label { Position = new Vector2(10, 2), Size = new Vector2(110, 18), Text = "职务 / 封号" };
        var colName = new Label { Position = new Vector2(125, 2), Size = new Vector2(130, 18), Text = "角色名" };
        var colJob  = new Label { Position = new Vector2(260, 2), Size = new Vector2(65, 18), Text = "职业" };
        var colLvl  = new Label { Position = new Vector2(330, 2), Size = new Vector2(55, 18), Text = "等级" };
        var colSts  = new Label { Position = new Vector2(390, 2), Size = new Vector2(65, 18), Text = "在线状态" };

        foreach (var lbl in new[] { colRole, colName, colJob, colLvl, colSts })
        {
            lbl.AddThemeFontSizeOverride("font_size", 11);
            lbl.AddThemeColorOverride("font_color", new Color(0.95f, 0.85f, 0.35f));
            headerContainer.AddChild(lbl);
        }

        // 分割线
        var sep = new ColorRect
        {
            Position = new Vector2(8, 28),
            Size = new Vector2(550, 1),
            Color = new Color(0.35f, 0.35f, 0.35f, 0.8f)
        };
        _memberListView.AddChild(sep);

        _memberListContainer = new VBoxContainer
        {
            Name = "MemberListContainer",
            Position = new Vector2(8, 34),
            Size = new Vector2(550, 300)
        };
        _memberListContainer.AddThemeConstantOverride("separation", 2);
        _memberListView.AddChild(_memberListContainer);

        // 底部成员总数与页码提示
        _lblMemberCount = new Label
        {
            Name = "LblMemberCount",
            Position = new Vector2(12, 342),
            Size = new Vector2(540, 18),
            Text = "成员总数: 0 | 第 1/1 页"
        };
        _lblMemberCount.AddThemeFontSizeOverride("font_size", 10);
        _lblMemberCount.AddThemeColorOverride("font_color", new Color(0.7f, 0.7f, 0.7f));
        _memberListView.AddChild(_lblMemberCount);
    }

    private void SetupButtons()
    {
        if (_panel == null) return;

        // 官方 1.5 行会右侧原生翻页按钮 (IDA sub_481dd4 [0x485db4..0x485e2d]):
        // 底板 Prguse_0180 右侧凸起圆盘已自带暗蓝底箭头凹槽，常态 TextureNormal 必须为 null 透出底板！
        // 仅在按下时 (TexturePressed) 显示亮色 0373(上)/0372(下) 高亮态！
        var texUp = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0373.png");
        var btnScrollUp = new TextureButton
        {
            Name = "BtnGuildScrollUp",
            Position = new Vector2(595, 239),
            Size = new Vector2(24, 23),
            TextureNormal = null,
            TexturePressed = texUp
        };
        btnScrollUp.Pressed += OnScrollUp;
        _panel.AddChild(btnScrollUp);

        var texDown = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0372.png");
        var btnScrollDown = new TextureButton
        {
            Name = "BtnGuildScrollDown",
            Position = new Vector2(595, 291),
            Size = new Vector2(24, 23),
            TextureNormal = null,
            TexturePressed = texDown
        };
        btnScrollDown.Pressed += OnScrollDown;
        _panel.AddChild(btnScrollDown);

        // 官方 1.5 行会底栏 11 大原生操作按钮 (Prguse_0182~0203 64x16 偶数Normal 奇数Pressed, IDA sub_481dd4 实证):
        // 第 1 行 (Top = 411):
        // - 主页 (198/199 @ 13, 411)
        _texHomeNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0198.png");
        _texHomePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0199.png");
        _btnHome = new TextureButton
        {
            Name = "BtnGuildHome",
            Position = new Vector2(13, 411),
            Size = new Vector2(64, 16),
            TextureNormal = _texHomePressed, // 默认处于主页，显示激活高亮态
            TexturePressed = _texHomePressed
        };
        _btnHome.Pressed += ShowHome;
        _panel.AddChild(_btnHome);

        // - 添加成员 (182/183 @ 243, 411)
        _btnAddMember = new TextureButton
        {
            Name = "BtnGuildAddMember",
            Position = new Vector2(243, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0182.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0183.png")
        };
        _btnAddMember.Pressed += OpenAddMemberDialog;
        _panel.AddChild(_btnAddMember);
        _adminButtons.Add(_btnAddMember);

        // - 编辑公告 (196/197 @ 325, 411)
        _btnEditNotice = new TextureButton
        {
            Name = "BtnGuildEditNotice",
            Position = new Vector2(325, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0196.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0197.png")
        };
        _btnEditNotice.Pressed += () => _editNoticeDlg?.Open(_currentRawNotice);
        _panel.AddChild(_btnEditNotice);
        _adminButtons.Add(_btnEditNotice);

        // - 联盟 (184/185 @ 407, 411)
        _btnAlly = new TextureButton
        {
            Name = "BtnGuildAlly",
            Position = new Vector2(407, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0184.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0185.png")
        };
        _btnAlly.Pressed += OpenAllyDialog;
        _panel.AddChild(_btnAlly);
        _adminButtons.Add(_btnAlly);

        // - 战争 (202/203 @ 529, 411)
        _btnWar = new TextureButton
        {
            Name = "BtnGuildWar",
            Position = new Vector2(529, 411),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0202.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0203.png")
        };
        _btnWar.Pressed += OpenWarDialog;
        _panel.AddChild(_btnWar);
        _adminButtons.Add(_btnWar);

        // 第 2 行 (Top = 429):
        // - 成员列表 (200/201 @ 13, 429)
        _texMembersNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0200.png");
        _texMembersPressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0201.png");
        _btnMembers = new TextureButton
        {
            Name = "BtnGuildMembers",
            Position = new Vector2(13, 429),
            Size = new Vector2(64, 16),
            TextureNormal = _texMembersNormal,
            TexturePressed = _texMembersPressed
        };
        _btnMembers.Pressed += ShowMemberList;
        _panel.AddChild(_btnMembers);

        // - 聊天记录 (190/191 @ 94, 429)
        var btnNotice = new TextureButton
        {
            Name = "BtnGuildChatHistory",
            Position = new Vector2(94, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0190.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0191.png")
        };
        _panel.AddChild(btnNotice);

        // - 删除成员 (192/193 @ 243, 429)
        _btnDelMember = new TextureButton
        {
            Name = "BtnGuildDelMember",
            Position = new Vector2(243, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0192.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0193.png")
        };
        _btnDelMember.Pressed += OpenDelMemberDialog;
        _panel.AddChild(_btnDelMember);
        _adminButtons.Add(_btnDelMember);

        // - 编辑封号 (194/195 @ 325, 429)
        _btnEditRank = new TextureButton
        {
            Name = "BtnGuildEditRank",
            Position = new Vector2(325, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0194.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0195.png")
        };
        _btnEditRank.Pressed += () =>
        {
            string selName = _selectedMember?.name ?? (_members.Count > 0 ? _members[0].name : "");
            string selRank = _selectedMember?.role ?? (_members.Count > 0 ? _members[0].role : "");
            _editRankDlg?.Open(selName, selRank, _members.Select(m => m.name));
        };
        _panel.AddChild(_btnEditRank);
        _adminButtons.Add(_btnEditRank);

        // - 取消联盟 (186/187 @ 407, 429)
        _btnCancelAlly = new TextureButton
        {
            Name = "BtnGuildCancelAlly",
            Position = new Vector2(407, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0186.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0187.png")
        };
        _btnCancelAlly.Pressed += OpenCancelAllyDialog;
        _panel.AddChild(_btnCancelAlly);
        _adminButtons.Add(_btnCancelAlly);

        // - 取消战争 (188/189 @ 529, 429)
        _btnCancelWar = new TextureButton
        {
            Name = "BtnGuildCancelWar",
            Position = new Vector2(529, 429),
            Size = new Vector2(64, 16),
            TextureNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0188.png"),
            TexturePressed = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0189.png")
        };
        _btnCancelWar.Pressed += OpenCancelWarDialog;
        _panel.AddChild(_btnCancelWar);
        _adminButtons.Add(_btnCancelWar);
    }

    public void SetMessageBoxDialog(MessageBoxDialog? msgBox)
    {
        _messageBoxDialog = msgBox;
    }

    /// <summary>
    /// 设置掌门人管理权限模式 (严格对齐官方 IDA sub_4930A0 [0x4930fa..0x4931fe])
    /// 会长/Admin 拥有全部 8 个特权管理按钮; 普通行会成员自动隐藏此 8 个按钮
    /// </summary>
    public void SetLeaderMode(bool isLeader)
    {
        IsLeader = isLeader;
        foreach (var btn in _adminButtons)
        {
            btn.Visible = isLeader;
        }
    }

    private MessageBoxDialog GetOrFindMessageBox()
    {
        if (_messageBoxDialog != null) return _messageBoxDialog;
        var msg = GetParent()?.GetNodeOrNull<MessageBoxDialog>("MessageBoxDialog");
        if (msg != null)
        {
            _messageBoxDialog = msg;
            return msg;
        }
        _messageBoxDialog = new MessageBoxDialog();
        AddChild(_messageBoxDialog);
        return _messageBoxDialog;
    }

    public void OpenAddMemberDialog()
    {
        var msgBox = GetOrFindMessageBox();
        msgBox.ShowInput("请输入要添加的行会成员名字:", "", (name) =>
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                EmitSignal(SignalName.AddMemberRequested, name.Trim());
            }
        });
    }

    public void OpenDelMemberDialog()
    {
        var msgBox = GetOrFindMessageBox();
        if (_selectedMember.HasValue)
        {
            string memberName = _selectedMember.Value.name;
            msgBox.ShowMessage($"确定要将成员 [{memberName}] 逐出行会吗？", true);
            MessageBoxDialog.ConfirmedEventHandler onConfirm = null!;
            onConfirm = () =>
            {
                msgBox.Confirmed -= onConfirm;
                EmitSignal(SignalName.DelMemberRequested, memberName);
            };
            msgBox.Confirmed += onConfirm;
        }
        else
        {
            msgBox.ShowInput("请输入要逐出的行会成员名字:", "", (name) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    EmitSignal(SignalName.DelMemberRequested, name.Trim());
                }
            });
        }
    }

    public void OpenAllyDialog()
    {
        var msgBox = GetOrFindMessageBox();
        msgBox.ShowInput("请输入要结盟的行会名称:", "", (guild) =>
        {
            if (!string.IsNullOrWhiteSpace(guild))
            {
                EmitSignal(SignalName.AllyRequested, guild.Trim());
            }
        });
    }

    public void OpenCancelAllyDialog()
    {
        var msgBox = GetOrFindMessageBox();
        msgBox.ShowInput("请输入要解除结盟的行会名称:", "", (guild) =>
        {
            if (!string.IsNullOrWhiteSpace(guild))
            {
                EmitSignal(SignalName.CancelAllyRequested, guild.Trim());
            }
        });
    }

    public void OpenWarDialog()
    {
        var msgBox = GetOrFindMessageBox();
        msgBox.ShowInput("请输入要宣战的行会名称:", "", (guild) =>
        {
            if (!string.IsNullOrWhiteSpace(guild))
            {
                EmitSignal(SignalName.WarRequested, guild.Trim());
            }
        });
    }

    public void OpenCancelWarDialog()
    {
        var msgBox = GetOrFindMessageBox();
        msgBox.ShowInput("请输入要停止战争的行会名称:", "", (guild) =>
        {
            if (!string.IsNullOrWhiteSpace(guild))
            {
                EmitSignal(SignalName.CancelWarRequested, guild.Trim());
            }
        });
    }

    private void OnScrollUp()
    {
        if (_memberListView != null && _memberListView.Visible)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                RenderCurrentPageMembers();
            }
        }
    }

    private void OnScrollDown()
    {
        if (_memberListView != null && _memberListView.Visible)
        {
            int maxPage = Math.Max(0, (_members.Count - 1) / PageSize);
            if (_currentPage < maxPage)
            {
                _currentPage++;
                RenderCurrentPageMembers();
            }
        }
    }

    /// <summary>
    /// 设置并刷新行会成员名单 (由 SM_SENDGUILDMEMBERLIST / SM_GUILDMEMBERLIST 驱动)
    /// </summary>
    public void SetMemberList(IEnumerable<(string role, string name, string job, int level, bool online)> members)
    {
        _members.Clear();
        _members.AddRange(members);
        _currentPage = 0;
        RenderCurrentPageMembers();
    }

    private void RenderCurrentPageMembers()
    {
        if (_memberListContainer == null) return;
        foreach (Node child in _memberListContainer.GetChildren())
        {
            child.QueueFree();
        }

        int totalCount = _members.Count;
        int maxPage = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        int curPageDisplay = Math.Min(_currentPage + 1, maxPage);

        if (_lblMemberCount != null)
        {
            _lblMemberCount.Text = $"成员总数: {totalCount} | 第 {curPageDisplay}/{maxPage} 页";
        }

        var pageItems = _members.Skip(_currentPage * PageSize).Take(PageSize);
        foreach (var m in pageItems)
        {
            AddMemberRow(m.role, m.name, m.job, m.level, m.online);
        }
    }

    private void AddMemberRow(string role, string name, string job, int level, bool online)
    {
        if (_memberListContainer == null) return;

        bool isSelected = _selectedMember.HasValue && _selectedMember.Value.name == name;

        var rowBtn = new Button
        {
            CustomMinimumSize = new Vector2(540, 20),
            Size = new Vector2(540, 20),
            Flat = !isSelected,
            FocusMode = FocusModeEnum.None
        };

        if (isSelected)
        {
            var sb = new StyleBoxFlat
            {
                BgColor = new Color(0.12f, 0.28f, 0.55f, 0.75f),
                CornerRadiusBottomLeft = 2,
                CornerRadiusBottomRight = 2,
                CornerRadiusTopLeft = 2,
                CornerRadiusTopRight = 2
            };
            rowBtn.AddThemeStyleboxOverride("normal", sb);
            rowBtn.AddThemeStyleboxOverride("hover", sb);
            rowBtn.AddThemeStyleboxOverride("pressed", sb);
        }
        else
        {
            rowBtn.AddThemeStyleboxOverride("normal", new StyleBoxEmpty());
            var hoverSb = new StyleBoxFlat
            {
                BgColor = new Color(0.2f, 0.25f, 0.35f, 0.4f),
                CornerRadiusBottomLeft = 2,
                CornerRadiusBottomRight = 2,
                CornerRadiusTopLeft = 2,
                CornerRadiusTopRight = 2
            };
            rowBtn.AddThemeStyleboxOverride("hover", hoverSb);
        }

        var lblRole = new Label
        {
            Position = new Vector2(10, 0),
            Size = new Vector2(110, 20),
            Text = role,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblRole.AddThemeFontSizeOverride("font_size", 10);
        lblRole.AddThemeColorOverride("font_color", new Color(1.0f, 0.88f, 0.3f));

        var lblName = new Label
        {
            Position = new Vector2(125, 0),
            Size = new Vector2(130, 20),
            Text = name,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblName.AddThemeFontSizeOverride("font_size", 10);
        lblName.AddThemeColorOverride("font_color", isSelected ? new Color(1f, 1f, 0.6f) : new Color(0.95f, 0.95f, 0.95f));

        var lblJob = new Label
        {
            Position = new Vector2(260, 0),
            Size = new Vector2(65, 20),
            Text = job,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblJob.AddThemeFontSizeOverride("font_size", 10);
        Color jobColor = job switch
        {
            "战士" => new Color(0.95f, 0.5f, 0.5f),
            "法师" => new Color(0.5f, 0.75f, 1.0f),
            "道士" => new Color(0.5f, 0.95f, 0.5f),
            _ => new Color(0.85f, 0.85f, 0.85f)
        };
        lblJob.AddThemeColorOverride("font_color", jobColor);

        var lblLvl = new Label
        {
            Position = new Vector2(330, 0),
            Size = new Vector2(55, 20),
            Text = level.ToString(),
            VerticalAlignment = VerticalAlignment.Center
        };
        lblLvl.AddThemeFontSizeOverride("font_size", 10);
        lblLvl.AddThemeColorOverride("font_color", new Color(0.85f, 0.85f, 0.85f));

        var lblSts = new Label
        {
            Position = new Vector2(390, 0),
            Size = new Vector2(65, 20),
            Text = online ? "在线" : "离线",
            VerticalAlignment = VerticalAlignment.Center
        };
        lblSts.AddThemeFontSizeOverride("font_size", 10);
        lblSts.AddThemeColorOverride("font_color", online ? new Color(0.2f, 1.0f, 0.3f) : new Color(0.55f, 0.55f, 0.55f));

        rowBtn.AddChild(lblRole);
        rowBtn.AddChild(lblName);
        rowBtn.AddChild(lblJob);
        rowBtn.AddChild(lblLvl);
        rowBtn.AddChild(lblSts);

        rowBtn.Pressed += () =>
        {
            _selectedMember = (role, name, job, level, online);
            RenderCurrentPageMembers();
        };

        _memberListContainer.AddChild(rowBtn);
    }

    public void ShowHome()
    {
        if (_homeView != null) _homeView.Visible = true;
        if (_memberListView != null) _memberListView.Visible = false;
        if (_btnHome != null && _texHomePressed != null) _btnHome.TextureNormal = _texHomePressed;
        if (_btnMembers != null && _texMembersNormal != null) _btnMembers.TextureNormal = _texMembersNormal;
        EmitSignal(SignalName.HomeRequested);
    }

    public void ShowMemberList()
    {
        if (_homeView != null) _homeView.Visible = false;
        if (_memberListView != null) _memberListView.Visible = true;
        if (_btnHome != null && _texHomeNormal != null) _btnHome.TextureNormal = _texHomeNormal;
        if (_btnMembers != null && _texMembersPressed != null) _btnMembers.TextureNormal = _texMembersPressed;
        RenderCurrentPageMembers();
        EmitSignal(SignalName.MemberListRequested);
    }

    public void SetGuildName(string guildName)
    {
        if (_lblGuildTitle != null)
        {
            _lblGuildTitle.Text = guildName;
        }
    }

    public void TogglePanel()
    {
        Visible = !Visible;
        if (Visible)
        {
            ShowHome();
        }
        else
        {
            if (_editNoticeDlg != null) _editNoticeDlg.Visible = false;
            if (_editRankDlg != null) _editRankDlg.Visible = false;
        }
    }

    /// <summary>
    /// 解析并展示服务端下发的原始行会主页文本 (盛大官方 1.5 IDA sub_46E0B8 实证):
    /// 服务端报文按换行符和 "<Notice>", "<KillGuilds>", "<AllyGuilds>" 切分为三大段落,
    /// 标题 "公告"、"敌对行会"、"联盟行会" 严格以纯白 0xFFFFFF (IDA 0x46e218/0x46e27a/0x46e30a) 输出,
    /// 内容逐行排版。
    /// </summary>
    public void UpdateHomeNotice(string rawContent)
    {
        _currentRawNotice = rawContent;
        var (notices, killGuilds, allyGuilds) = ParseHomeNotice(rawContent);
        SetHomeText(notices, killGuilds, allyGuilds);
    }

    public static (List<string> notices, List<string> killGuilds, List<string> allyGuilds) ParseHomeNotice(string rawContent)
    {
        var notices = new List<string>();
        var killGuilds = new List<string>();
        var allyGuilds = new List<string>();

        if (!string.IsNullOrEmpty(rawContent))
        {
            int section = 1; // 默认第一段为公告 (IDA 0x46e1c0 v10=0)
            var lines = rawContent.Replace("\r\n", "\n").Split('\n');
            foreach (var rawLine in lines)
            {
                string line = rawLine.Trim();
                if (line.Equals("<Notice>", StringComparison.OrdinalIgnoreCase))
                {
                    section = 1;
                    continue;
                }
                if (line.Equals("<KillGuilds>", StringComparison.OrdinalIgnoreCase))
                {
                    section = 2;
                    continue;
                }
                if (line.Equals("<AllyGuilds>", StringComparison.OrdinalIgnoreCase))
                {
                    section = 3;
                    continue;
                }
                if (string.IsNullOrEmpty(line)) continue;

                switch (section)
                {
                    case 1:
                        notices.Add(line);
                        break;
                    case 2:
                        killGuilds.Add(line);
                        break;
                    case 3:
                        allyGuilds.Add(line);
                        break;
                }
            }
        }

        return (notices, killGuilds, allyGuilds);
    }

    /// <summary>
    /// 行会主页文本 (盛大官方 1.5 格式 IDA sub_46E0B8 实证):
    /// 包含 "<Notice>", "<KillGuilds>", "<AllyGuilds>" 三大段落,
    /// 段落标题 "公告"、"敌对行会"、"联盟行会" 严格以 0xFFFFFF (纯白) 高亮输出,
    /// 段内文本换行呈现, 敌对/联盟行会逐行展示。
    /// </summary>
    public void SetHomeText(IEnumerable<string> notice, IEnumerable<string> killGuilds, IEnumerable<string> allyGuilds)
    {
        if (_noticeText == null) return;
        var sb = new System.Text.StringBuilder();
        sb.Append("[color=#ffffff]公告[/color]\n");
        bool hasNotice = false;
        foreach (var l in notice)
        {
            sb.Append("[color=#d4d4d4]").Append(l).Append("[/color]\n");
            hasNotice = true;
        }
        if (!hasNotice) sb.Append("[color=#888888]（暂无公告）[/color]\n");

        sb.Append("\n[color=#ffffff]敌对行会[/color]\n");
        bool hasKill = false;
        foreach (var l in killGuilds)
        {
            sb.Append("[color=#ff6666]").Append(l).Append("[/color]\n");
            hasKill = true;
        }
        if (!hasKill) sb.Append("[color=#888888]（无）[/color]\n");

        sb.Append("\n[color=#ffffff]联盟行会[/color]\n");
        bool hasAlly = false;
        foreach (var l in allyGuilds)
        {
            sb.Append("[color=#66ccff]").Append(l).Append("[/color]\n");
            hasAlly = true;
        }
        if (!hasAlly) sb.Append("[color=#888888]（无）[/color]\n");

        _noticeText.Text = sb.ToString();
    }

    private void OnClose()
    {
        Visible = false;
        if (_editNoticeDlg != null) _editNoticeDlg.Visible = false;
        if (_editRankDlg != null) _editRankDlg.Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
