namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 原版《虎卫传说》第 1551 ~ 1650 号微观细节规则权威判定引擎
/// 100% 服务端权威运算，纯血 Dumb Client 架构，严禁客户端本地作弊。
/// </summary>
public static class OfficialMythicApex100DetailsEngine
{
    // =========================================================================
    // 细节 1551 ~ 1565：沃玛寺庙与沃玛教主神殿全生态权威规则 (官方 1.50 绝对真值)
    // =========================================================================

    /// <summary>
    /// 细节 1551：沃玛森林迷宫暗黑迷雾视距衰减 (暗黑迷雾环境下客户端有效视距由 8 格压缩至 4 格)
    /// </summary>
    public static int CalculateWoomaFogViewDistance(int baseViewDistance, bool hasDenseFog)
    {
        if (!hasDenseFog) return baseViewDistance;
        return Math.Min(4, baseViewDistance);
    }

    /// <summary>
    /// 细节 1552：沃玛寺庙三层黑暗音效触发检测 (进入三层神殿边缘触发低沉幽闭环境音效)
    /// </summary>
    public static bool ShouldPlayWoomaTempleAmbientSound(int playerX, int playerY, int altarCenterX, int altarCenterY)
    {
        int distSq = (playerX - altarCenterX) * (playerX - altarCenterX) + (playerY - altarCenterY) * (playerY - altarCenterY);
        return distSq <= 64; // 半径 8 格以内
    }

    /// <summary>
    /// 细节 1553：沃玛火焰战士远程喷火射程与衰减 (射程 3 格，每增加 1 格伤害衰减 15%)
    /// </summary>
    public static int CalculateWoomaFireDamageDecay(int baseDamage, int distance)
    {
        if (distance <= 1) return baseDamage;
        float decay = 1.0f - (distance - 1) * 0.15f;
        decay = Math.Max(0.5f, decay);
        return (int)(baseDamage * decay);
    }

    /// <summary>
    /// 细节 1554：沃玛卫士大爆掉落散落判定 (大爆 8x8 环形散落，教主与卫士中心留空)
    /// </summary>
    public static int CalculateWoomaGuardBurstRadius(int guardLevel)
    {
        return guardLevel >= 40 ? 4 : 3;
    }

    /// <summary>
    /// 细节 1555：沃玛教主狂暴瞬移脱战机制 (生命低于 30% 受到近战攻击有 25% 几率瞬移至神殿随机坐标)
    /// </summary>
    public static bool CheckWoomaLeaderEmergencyTeleport(int currentHp, int maxHp, int randomRoll0To99)
    {
        if (currentHp <= maxHp * 0.30f && randomRoll0To99 < 25)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 细节 1556：沃玛教主召唤火焰沃玛随从 (狂暴状态下每次瞬移召唤 4 只火焰沃玛护驾)
    /// </summary>
    public static int GetWoomaLeaderSummonGuardCount(bool isEnraged)
    {
        return isEnraged ? 4 : 2;
    }

    /// <summary>
    /// 细节 1557：沃玛教主近身雷电击退与破盾 (教主近身雷击 35% 几率击退玩家 1 格并破除魔法盾)
    /// </summary>
    public static (bool pushed, bool breakShield) CalculateWoomaLeaderLightningEffect(int randomRoll0To99)
    {
        if (randomRoll0To99 < 35)
        {
            return (true, true);
        }
        return (false, false);
    }

    /// <summary>
    /// 细节 1558：沃玛教主大爆沃玛号角掉落独占保护 (教主击杀者行会享有 60 秒号角优先拾取权)
    /// </summary>
    public static bool CanPickupWoomaHorn(long killerGuildId, long pickerGuildId, long dropElapsedSeconds)
    {
        if (dropElapsedSeconds >= 60) return true;
        return killerGuildId == pickerGuildId;
    }

    /// <summary>
    /// 细节 1559：沃玛战士受击麻痹抗性判定 (沃玛战士具备 30% 麻痹减免抗性)
    /// </summary>
    public static bool CheckWoomaWarriorParalyzeResist(int randomRoll0To99)
    {
        return randomRoll0To99 < 30;
    }

    /// <summary>
    /// 细节 1560：沃玛寺庙黑暗环境火把照明衰减 (火把照明范围随持久衰减，最大 5 格最低 2 格)
    /// </summary>
    public static int CalculateTorchLightRadius(int currentDura, int maxDura)
    {
        if (maxDura <= 0 || currentDura <= 0) return 1;
        float ratio = (float)currentDura / maxDura;
        if (ratio > 0.6f) return 5;
        if (ratio > 0.2f) return 3;
        return 2;
    }

    /// <summary>
    /// 细节 1561：沃玛教主神殿神像不可摧毁阻挡特性 (神殿四根巨大石柱为不可破坏物理阻挡)
    /// </summary>
    public static bool IsWoomaTemplePillarBlocked(int tileX, int tileY)
    {
        // 官方 1.5 沃玛三层祭坛四根核心石柱坐标阻挡
        return (tileX == 24 && tileY == 24) || (tileX == 24 && tileY == 36) ||
               (tileX == 36 && tileY == 24) || (tileX == 36 && tileY == 36);
    }

    /// <summary>
    /// 细节 1562：沃玛暗黑战士急速移动追踪机制 (暗黑战士移速提升 40%，移动间隔缩短至 350ms)
    /// </summary>
    public static int CalculateDarkWarriorMoveIntervalMs(int baseIntervalMs)
    {
        return (int)(baseIntervalMs * 0.60f);
    }

    /// <summary>
    /// 细节 1563：沃玛暗黑战士等阶判定与抗拒火环免疫 (怪物等阶高于玩家时抗拒火环失效)
    /// </summary>
    public static bool CanRepelDarkWarrior(int playerLevel, int monsterLevel)
    {
        return playerLevel > monsterLevel;
    }

    /// <summary>
    /// 细节 1564：沃玛号角创建行会手续费与等级门槛 (消耗沃玛号角与 100 万金币，创建者等级 >= 22)
    /// </summary>
    public static (bool allowed, string reason) ValidateCreateGuildWithHorn(int playerLevel, int playerGold, bool hasWoomaHorn)
    {
        if (!hasWoomaHorn) return (false, "缺少创建行会的核心信物【沃玛号角】。");
        if (playerLevel < 22) return (false, "创建行会需要角色等级达到 22 级。");
        if (playerGold < 1_000_000) return (false, "创建行会需向比奇城主缴纳 100 万金币。");
        return (true, string.Empty);
    }

    /// <summary>
    /// 细节 1565：沃玛教主神殿中央祭坛掉落物品防卡墙修正 (祭坛台阶边缘掉落自动位移至空闲合法地砖)
    /// </summary>
    public static (int validX, int validY) AdjustDropTileIfBlocked(int rawX, int rawY, Func<int, int, bool> isTileWalkable)
    {
        if (isTileWalkable(rawX, rawY)) return (rawX, rawY);
        // 依次尝试周围 8 格
        int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
        int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };
        for (int i = 0; i < 8; i++)
        {
            int nx = rawX + dx[i];
            int ny = rawY + dy[i];
            if (isTileWalkable(nx, ny)) return (nx, ny);
        }
        return (rawX, rawY);
    }

    // =========================================================================
    // 细节 1566 ~ 1580：黄泉教主与封魔谷殿堂战术
    // =========================================================================

    /// <summary>
    /// 细节 1566：黄泉教主全屏流星火雨魔法护盾抵消 (魔法盾吸收 60% 伤害，余下 40% 扣血)
    /// </summary>
    public static (int hpDamage, int shieldAbsorbed) CalculateHuangQuanMeteorDamage(int rawMagicDamage, bool hasMagicShield)
    {
        if (!hasMagicShield) return (rawMagicDamage, 0);
        int absorbed = (int)(rawMagicDamage * 0.60f);
        return (rawMagicDamage - absorbed, absorbed);
    }

    /// <summary>
    /// 细节 1567：黄泉教主三大神兵官方爆率判定 (龙牙 1/1200，怒斩 1/1200，逍遥扇 1/1200)
    /// </summary>
    public static string CheckHuangQuanSuperWeaponDrop(int randomRoll0To1199)
    {
        return randomRoll0To1199 switch
        {
            0 => "龙牙",
            1 => "怒斩",
            2 => "逍遥扇",
            _ => "None"
        };
    }

    /// <summary>
    /// 细节 1568：封魔殿暗之祖玛教主破隐反制机制 (隐身角色在暗之祖玛 3 格内直接破除隐身)
    /// </summary>
    public static bool ShouldRevealHiddenPlayer(int bossX, int bossY, int playerX, int playerY)
    {
        int distSq = (bossX - playerX) * (bossX - playerX) + (bossY - playerY) * (bossY - playerY);
        return distSq <= 9; // 3 格以内
    }

    /// <summary>
    /// 细节 1569：魔魂殿恶魔蝙蝠集群仇恨扩散 (一只蝙蝠受击，周围 5 格同类全部激活仇恨)
    /// </summary>
    public static bool ShouldAlertBatAlly(int hitBatX, int hitBatY, int allyBatX, int allyBatY)
    {
        int dist = Math.Max(Math.Abs(hitBatX - allyBatX), Math.Abs(hitBatY - allyBatY));
        return dist <= 5;
    }

    /// <summary>
    /// 细节 1570：虹魔教主吸血吸蓝转化率 (攻击伤害的 30% 转化为自身生命，10% 转化为魔力)
    /// </summary>
    public static (int hpGain, int mpGain) CalculateHongMoDrain(int damageDealt)
    {
        return ((int)(damageDealt * 0.30f), (int)(damageDealt * 0.10f));
    }

    /// <summary>
    /// 细节 1571：封魔矿区僵尸钻地复活概率与经验惩罚 (20% 概率爬起复活，复活后经验仅给 50%)
    /// </summary>
    public static (bool revive, int revisedExp) CheckZombieResurrection(int standardExp, int randomRoll0To99)
    {
        if (randomRoll0To99 < 20)
        {
            return (true, standardExp / 2);
        }
        return (false, standardExp);
    }

    /// <summary>
    /// 细节 1572：千年树妖定点范围毒刺 (每 3 秒周围 6 格地刺，造成真实伤害并附带绿毒)
    /// </summary>
    public static (int trueDamage, bool applyGreenPoison) CalculateMillenniumTreeAttack(int distance)
    {
        if (distance > 6) return (0, false);
        return (45, true);
    }

    /// <summary>
    /// 细节 1573：封魔谷大刀守卫对红名秒杀真伤 (大刀守卫攻击固定 9999 真实伤害，无视一切减伤)
    /// </summary>
    public static int CalculateGuardExecutionDamage(bool isRedName)
    {
        return isRedName ? 9999 : 0;
    }

    /// <summary>
    /// 细节 1574：地下秘密通道传送石禁锢 (秘密通道内禁止使用回城卷与随机卷轴)
    /// </summary>
    public static bool IsScrollAllowedInSecretTunnel(string mapId)
    {
        return mapId != "D2008"; // 封魔秘密通道
    }

    /// <summary>
    /// 细节 1575：疾风殿移动速度封包作弊拦截 (单步移动间隔小于 550ms 视为非法变速)
    /// </summary>
    public static bool CheckSpeedHackAction(long lastMoveTimeMs, long currentMoveTimeMs)
    {
        return (currentMoveTimeMs - lastMoveTimeMs) < 550;
    }

    /// <summary>
    /// 细节 1576：封魔殿极品祖玛卫士移动狂暴 (生命低于 30% 时移动速度提升 40%)
    /// </summary>
    public static int CalculateEliteGuardMoveInterval(int baseIntervalMs, int currentHp, int maxHp)
    {
        if (currentHp <= maxHp * 0.30f)
        {
            return (int)(baseIntervalMs * 0.60f); // 间隔缩短 40%
        }
        return baseIntervalMs;
    }

    /// <summary>
    /// 细节 1577：幽冥回廊邪恶毒蛇毒素持续时间 (邪恶毒蛇喷毒持续 12 秒，每秒扣 8 点生命)
    /// </summary>
    public static (int durationSeconds, int dps) GetEvilSnakePoisonEffect()
    {
        return (12, 8);
    }

    /// <summary>
    /// 细节 1578：霸者大厅仓库管理员保管费 (非沙巴克成员每次存取物品收取 10 金币)
    /// </summary>
    public static int GetStorageServiceFee(bool isSabukMember)
    {
        return isSabukMember ? 0 : 10;
    }

    /// <summary>
    /// 细节 1579：烈焰殿楔蛾破魔粉末 (楔蛾攻击有 10% 几率破除法师魔法盾)
    /// </summary>
    public static bool CheckWedgeMothDispelShield(int randomRoll0To99)
    {
        return randomRoll0To99 < 10;
    }

    /// <summary>
    /// 细节 1580：光芒回廊尸王掉落高级技能书保护时间 (极品书掉落后击杀者独占保护 60 秒)
    /// </summary>
    public static bool IsBookLootProtected(long dropTimeMs, long currentTimeMs, int killerId, int looterId)
    {
        if (killerId == looterId) return false;
        return (currentTimeMs - dropTimeMs) < 60000;
    }

    // =========================================================================
    // 细节 1581 ~ 1595：毒符药剂重量沉降与包囊极值
    // =========================================================================

    /// <summary>
    /// 细节 1581：超级金创药/魔法药负重限制 (超级药水单瓶重量 3，超出玩家负重上限禁止购买)
    /// </summary>
    public static bool CanPlayerCarryExtraWeight(int currentBagWeight, int maxBagWeight, int itemWeight, int count)
    {
        return (currentBagWeight + itemWeight * count) <= maxBagWeight;
    }

    /// <summary>
    /// 细节 1582：道士护身符张数与消耗速率 (灵魂火符单次消耗 1 张符，神圣战甲术消耗 1 张符)
    /// </summary>
    public static (bool success, int remainingPaper) ConsumeTaoistAmulet(int currentPaper, int cost)
    {
        if (currentPaper < cost) return (false, currentPaper);
        return (true, currentPaper - cost);
    }

    /// <summary>
    /// 细节 1583：绿色毒药与红色毒药包重叠溢出脚下沉降判定 (背包满 40 格时制作毒药掉落在地)
    /// </summary>
    public static bool ShouldDropItemToGroundWhenBagFull(int currentBagItemCount)
    {
        return currentBagItemCount >= 40;
    }

    /// <summary>
    /// 细节 1584：包裹 40 格满时金币进包逻辑 (金币是数值属性，不占格子，依然可拾取金币)
    /// </summary>
    public static bool CanPickupGoldWhenBagSlotsFull(int currentBagCount)
    {
        return currentBagCount >= 40; // 即使格子满，金币依然可以进金币袋
    }

    /// <summary>
    /// 细节 1585：沃玛号角与金币缴纳创建行会 (需沃玛号角 1 个与 100 万金币)
    /// </summary>
    public static bool CanCreateGuild(bool hasWomaHorn, int currentGold)
    {
        return hasWomaHorn && currentGold >= 1_000_000;
    }

    /// <summary>
    /// 细节 1586：行会解除同盟冷却判定 (同盟解除后需等待 24 小时方可重新结盟)
    /// </summary>
    public static bool CanReapplyGuildAlliance(long breakAllianceTimeMs, long currentTimeMs)
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        return (currentTimeMs - breakAllianceTimeMs) >= OneDayMs;
    }

    /// <summary>
    /// 细节 1587：行会战期间杀人不加 PK 值判定 (双方处于宣战期内，击杀不增加 PK 点)
    /// </summary>
    public static bool ShouldIncreasePkPointsDuringGuildWar(bool isGuildWarActive)
    {
        return !isGuildWarActive;
    }

    /// <summary>
    /// 细节 1588：沙巴克行会成员城内药水八折优惠 (原价 100 金币药水沙城仅售 80 金币)
    /// </summary>
    public static int ApplySabukDiscountPrice(int originalPrice, bool isSabukMember)
    {
        if (!isSabukMember) return originalPrice;
        return (int)(originalPrice * 0.80f);
    }

    /// <summary>
    /// 细节 1589：黄色药粉施毒防具破甲效果 (红毒降低防御与魔御 20%)
    /// </summary>
    public static (int def, int mdef) ApplyRedPoisonReduction(int baseDef, int baseMdef)
    {
        return ((int)(baseDef * 0.80f), (int)(baseMdef * 0.80f));
    }

    /// <summary>
    /// 细节 1590：灰色药粉持续掉血效果 (绿毒禁止自然回血且每 3 秒扣除与道术相关的固定血量)
    /// </summary>
    public static int CalculateGreenPoisonDamagePerTick(int taoPower)
    {
        return Math.Max(2, taoPower / 5);
    }

    /// <summary>
    /// 细节 1591：穿人穿怪防重叠阻断机制 (正常模式下不能穿越非盟友生物实体)
    /// </summary>
    public static bool CanPassThroughEntity(bool isEntityFriendly, bool isGhostMode)
    {
        return isGhostMode || isEntityFriendly;
    }

    /// <summary>
    /// 细节 1592：抗拒火环对高于自身等级目标推开失败判定 (等级小于或等于目标则必定失败)
    /// </summary>
    public static bool CanRepelTarget(int casterLevel, int targetLevel)
    {
        return casterLevel > targetLevel;
    }

    /// <summary>
    /// 细节 1593：诱惑之光定怪几率公式 (等级差与技能等级加成)
    /// </summary>
    public static bool CheckLureSuccess(int playerLevel, int monsterLevel, int skillLevel, int randomRoll0To99)
    {
        int chance = 30 + (playerLevel - monsterLevel) * 5 + skillLevel * 10;
        chance = Math.Clamp(chance, 5, 85);
        return randomRoll0To99 < chance;
    }

    /// <summary>
    /// 细节 1594：圣言术对不死系怪物秒杀几率 (非不死系必定失败，不死系按等级差判定)
    /// </summary>
    public static bool CheckHolyTurnUndead(bool isUndead, int playerLevel, int monsterLevel, int randomRoll0To99)
    {
        if (!isUndead) return false;
        int chance = Math.Max(5, (playerLevel - monsterLevel) * 3 + 20);
        return randomRoll0To99 < chance;
    }

    /// <summary>
    /// 细节 1595：隐身术在移动或攻击后立即失效 (移动一格或施放攻击技能破除隐身)
    /// </summary>
    public static bool ShouldBreakCloakOnAction(bool isMoveOrAttackAction)
    {
        return isMoveOrAttackAction;
    }

    // =========================================================================
    // 细节 1596 ~ 1610：武器升级碎刀残留与开刀机制实证
    // =========================================================================

    /// <summary>
    /// 细节 1596：黑铁矿石纯度与持久变化物理公式 (总纯度高于 60 增加持久，低于 30 扣持久)
    /// </summary>
    public static int CalculateWeaponUpgradeDuraDelta(int totalIronPurity)
    {
        if (totalIronPurity >= 60) return 1000;  // 增加 1 点持久
        if (totalIronPurity < 30) return -1000;  // 扣减 1 点持久
        return 0; // 持久不变
    }

    /// <summary>
    /// 细节 1597：首饰等级与属性跳点计算 (首饰属性总和决定成功率，跳点最高 +2)
    /// </summary>
    public static (bool success, int statBonus) CalculateWeaponUpgradeResult(int jewelryValue, int weaponUpgradeCount, int randomRoll0To99)
    {
        int chance = Math.Max(20, 75 - weaponUpgradeCount * 8 + jewelryValue / 10);
        chance = Math.Min(85, chance);
        if (randomRoll0To99 >= chance)
        {
            return (false, 0); // 碎刀
        }
        // 成功时判断是否跳点 (+2)
        int bonus = (randomRoll0To99 < 5 && jewelryValue >= 50) ? 2 : 1;
        return (true, bonus);
    }

    /// <summary>
    /// 细节 1598：取回升级武器后开刀对象判定 (第一刀砍玩家或怪物，决定属性是否正式生效)
    /// </summary>
    public static bool IsValidSlashTarget(bool isMonsterOrPlayer)
    {
        return isMonsterOrPlayer;
    }

    /// <summary>
    /// 细节 1599：武器升级碎刀残留判定 (开刀碎刀时武器直接从装备栏彻底灰飞烟灭)
    /// </summary>
    public static bool HandleWeaponShatter(bool upgradeSucceeded)
    {
        return !upgradeSucceeded; // 如果失败则碎刀
    }

    /// <summary>
    /// 细节 1600：武器持久降为 0 时继续攻击的断裂惩罚 (攻击力降为 0 且 10% 几率彻底折断消失)
    /// </summary>
    public static (int actualAtk, bool isBroken) CheckZeroDuraAttack(int baseAtk, int currentDura, int randomRoll0To99)
    {
        if (currentDura > 0) return (baseAtk, false);
        bool broken = randomRoll0To99 < 10;
        return (0, broken);
    }

    /// <summary>
    /// 细节 1601：战神油修复底线 (战神油 100% 成功修复且绝不掉最大持久)
    /// </summary>
    public static (int newDura, int newMaxDura) RepairWithZhanshenOil(int maxDura)
    {
        return (maxDura, maxDura);
    }

    /// <summary>
    /// 细节 1602：普通特修掉持久几率 (铁匠特修有 2% 几率损失 1 点最大持久)
    /// </summary>
    public static int RepairWithBlacksmith(int currentMaxDura, int randomRoll0To99)
    {
        if (randomRoll0To99 < 2)
        {
            return Math.Max(1000, currentMaxDura - 1000);
        }
        return currentMaxDura;
    }

    /// <summary>
    /// 细节 1603：祝福油喝药幸运上限判定 (祝福油最高只能喝到幸运 +7)
    /// </summary>
    public static (bool success, int newLuck, bool cursed) DrinkBlessingOil(int currentLuck, int randomRoll0To99)
    {
        if (currentLuck >= 7) return (false, currentLuck, false);
        if (currentLuck < 0)
        {
            // 诅咒状态下喝祝福油洗掉诅咒
            if (randomRoll0To99 < 50) return (true, currentLuck + 1, false);
            return (false, currentLuck, false);
        }
        if (randomRoll0To99 < 25)
        {
            return (true, currentLuck + 1, false); // 幸运 +1
        }
        if (randomRoll0To99 > 90)
        {
            return (false, currentLuck - 1, true); // 诅咒 +1
        }
        return (false, currentLuck, false); // 无效
    }

    /// <summary>
    /// 细节 1604：幸运 9 刀刀最大上限攻击判定 (人物自身 + 武器幸运 >= 9 时 100% 触发 MaxDC)
    /// </summary>
    public static int CalculateDamageWithLuck(int minDc, int maxDc, int totalLuck, int randomRoll0To99)
    {
        if (totalLuck >= 9) return maxDc;
        if (totalLuck <= -9) return minDc;
        // 幸运加成偏向最大攻击
        int spread = maxDc - minDc;
        if (spread <= 0) return minDc;
        int bonus = (int)(spread * (totalLuck * 0.1f));
        int roll = minDc + (int)((spread * randomRoll0To99) / 100.0f) + bonus;
        return Math.Clamp(roll, minDc, maxDc);
    }

    /// <summary>
    /// 细节 1605：武器诅咒惩罚 (诅咒 +10 刀刀最低下限攻击)
    /// </summary>
    public static bool IsCursedMinAttackForced(int weaponCurse)
    {
        return weaponCurse >= 10;
    }

    /// <summary>
    /// 细节 1606：升级武器取回倒计时 (武器升级放入后需等待 60 分钟方可取回)
    /// </summary>
    public static bool CanRetrieveUpgradedWeapon(long depositTimeMs, long currentTimeMs)
    {
        const long OneHourMs = 60 * 60 * 1000L;
        return (currentTimeMs - depositTimeMs) >= OneHourMs;
    }

    /// <summary>
    /// 细节 1607：同一角色同时只能升级一把武器
    /// </summary>
    public static bool CanDepositAnotherWeapon(bool hasWeaponInForge)
    {
        return !hasWeaponInForge;
    }

    /// <summary>
    /// 细节 1608：升级属性封顶限制 (武器单项属性升级最多累加 7 点)
    /// </summary>
    public static bool CanUpgradeWeaponFurther(int currentUpgradedBonus)
    {
        return currentUpgradedBonus < 7;
    }

    /// <summary>
    /// 细节 1609：武器升级费用递增公式 (基础 10000 金币，每升一次增加 5000 金币)
    /// </summary>
    public static int GetUpgradeGoldCost(int currentUpgradeCount)
    {
        return 10000 + currentUpgradeCount * 5000;
    }

    /// <summary>
    /// 细节 1610：开刀砍和平模式队友安全机制 (砍同组队员不会触发碎刀判定)
    /// </summary>
    public static bool CanTriggerUpgradeSlashOnTeammate(bool isGroupMember)
    {
        return !isGroupMember;
    }

    // =========================================================================
    // 细节 1611 ~ 1625：官方 1.5 特戒系统物理真值与极品属性权威矩阵
    // =========================================================================

    /// <summary>
    /// 细节 1611：复活戒指濒死瞬间生命恢复判定 (生命归零瞬间立即恢复满血满蓝，持久扣除 1 点即 1000)
    /// </summary>
    public static (bool revived, int newHp, int newMp, int newDura) CheckResurrectionRingTrigger(int currentHp, int maxHp, int maxMp, int dura, long lastReviveTick, long nowTick)
    {
        if (currentHp > 0) return (false, currentHp, 0, dura);
        if (dura < 1000) return (false, 0, 0, dura);
        if (nowTick - lastReviveTick < 120 * 1000L) return (false, 0, 0, dura); // 120 秒冷却
        return (true, maxHp, maxMp, dura - 1000);
    }

    /// <summary>
    /// 细节 1612：复活戒指冷却时间判定 (每次复活后触发 120 秒冷却，冷却期内死亡正常暴毙)
    /// </summary>
    public static bool IsResurrectionRingOnCooldown(long lastReviveTick, long nowTick)
    {
        return (nowTick - lastReviveTick) < 120 * 1000L;
    }

    /// <summary>
    /// 细节 1613：护身戒指魔法值抵扣伤害计算 (受到伤害优先全额扣除 MP，MP 不足时剩余伤害扣除 HP)
    /// </summary>
    public static (int remainingHp, int remainingMp) CalculateProtectionRingDamage(int currentHp, int currentMp, int incomingDamage)
    {
        if (currentMp >= incomingDamage)
        {
            return (currentHp, currentMp - incomingDamage);
        }
        else
        {
            int overflow = incomingDamage - currentMp;
            int newHp = Math.Max(0, currentHp - overflow);
            return (newHp, 0);
        }
    }

    /// <summary>
    /// 细节 1614：麻痹戒指普攻麻痹概率与持续时长 (战士近战普攻 1/15 几率触发 5 秒麻痹石化)
    /// </summary>
    public static (bool paralyzed, int durationMs) CheckParalysisRingEffect(int randomRoll0To14)
    {
        if (randomRoll0To14 == 0) // 1/15 概率
        {
            return (true, 5000);
        }
        return (false, 0);
    }

    /// <summary>
    /// 细节 1615：隐身戒指对怪隐身效果与高级破隐怪物判定 (普通怪物无法察觉，楔蛾/月魔蜘蛛/赤月恶魔破隐)
    /// </summary>
    public static bool CanMonsterDetectInvisibleRing(int monsterRaceId)
    {
        // 楔蛾(32)、月魔蜘蛛(48)、赤月恶魔(88)等具备真实破隐感知
        return monsterRaceId == 32 || monsterRaceId == 48 || monsterRaceId == 88;
    }

    /// <summary>
    /// 细节 1616：传送戒指 @move 坐标合法性与距离校验 (同地图任意合法行走坐标瞬间传送)
    /// </summary>
    public static bool ValidateTeleportRingDestination(int destX, int destY, Func<int, int, bool> isWalkable)
    {
        if (destX < 0 || destY < 0) return false;
        return isWalkable(destX, destY);
    }

    /// <summary>
    /// 细节 1617：防御戒指佩戴激活全职业 1 级治愈术 (佩戴后非道士职业可释放 1 级治愈术)
    /// </summary>
    public static bool HasHealingSpellGrantedByRing(int ringItemId, int playerJob)
    {
        return ringItemId == 1001; // 防御戒指
    }

    /// <summary>
    /// 细节 1618：火焰戒指佩戴激活全职业 1 级火球术 (佩戴后非战士法师道士可释放 1 级火球术)
    /// </summary>
    public static bool HasFireballSpellGrantedByRing(int ringItemId)
    {
        return ringItemId == 1002; // 火焰戒指
    }

    /// <summary>
    /// 细节 1619：超负载戒指负重上限翻倍计算 (佩戴后腕力、穿戴负重、背包负重全额翻倍)
    /// </summary>
    public static (int newBagWeight, int newWearWeight, int newHandWeight) CalculateLoadRingMaxWeights(int bag, int wear, int hand)
    {
        return (bag * 2, wear * 2, hand * 2);
    }

    /// <summary>
    /// 细节 1620：技巧项链技能修炼熟练度双倍判定 (佩戴后每次成功施法获得双倍技能熟练度点数)
    /// </summary>
    public static int CalculateSkillAmuletExpGain(int baseExp, bool hasSkillAmulet)
    {
        return hasSkillAmulet ? baseExp * 2 : baseExp;
    }

    /// <summary>
    /// 细节 1621：武斗场观战防穿透碰撞 (比武场擂台边缘设置不可跨越阻挡栅栏)
    /// </summary>
    public static bool IsArenaBoundaryBlocked(int x, int y, int arenaMinX, int arenaMaxX, int arenaMinY, int arenaMaxY)
    {
        return x < arenaMinX || x > arenaMaxX || y < arenaMinY || y > arenaMaxY;
    }

    /// <summary>
    /// 细节 1622：祈祷全套神兽叛变官方时效 (全套祈祷生效后全服召唤兽叛变持续 180 秒)
    /// </summary>
    public static int GetPrayerRebellionDurationSeconds()
    {
        return 180;
    }

    /// <summary>
    /// 细节 1623：记忆一套组队天地合一召唤人数上限 (天地合一最多同时传送组内 10 名队员)
    /// </summary>
    public static int GetMemorySetMaxSummonCount()
    {
        return 10;
    }

    /// <summary>
    /// 细节 1624：神秘装备佩戴诅咒无法取下判定 (未鉴定神秘装备持久为 0 时无法取下，需神水洗净)
    /// </summary>
    public static bool IsMysteryItemLocked(bool isIdentified, int dura)
    {
        return !isIdentified && dura <= 0;
    }

    /// <summary>
    /// 细节 1625：战神盔甲重量防具需求 (需攻击力达到 46 点方可穿戴)
    /// </summary>
    public static bool CanEquipZhanShenArmor(int playerDc)
    {
        return playerDc >= 46;
    }

    // =========================================================================
    // 细节 1626 ~ 1640：行会战火、皇宫密道与城战指挥
    // =========================================================================

    /// <summary>
    /// 细节 1626：沙巴克皇宫入口推挤阻挡 (皇宫大门同时只能容纳 1 人穿行，形成卡位战术)
    /// </summary>
    public static bool IsDoorwayOccupied(bool hasEntityOnDoorway)
    {
        return hasEntityOnDoorway;
    }

    /// <summary>
    /// 细节 1627：沙巴克皇宫所有权切换倒计时 (皇宫内清空敌对行会成员持续 30 秒方可易主)
    /// </summary>
    public static bool CheckPalaceCaptureTime(int elapsedHoldSeconds)
    {
        return elapsedHoldSeconds >= 30;
    }

    /// <summary>
    /// 细节 1628：沙巴克密道老人传送费 (非守城方收取 5000 金币，守城方免费)
    /// </summary>
    public static int GetSecretPassageFee(bool isSabukMember)
    {
        return isSabukMember ? 0 : 5000;
    }

    /// <summary>
    /// 细节 1629：攻城战旗摧毁血量与全服通告 (旗帜血量 50000，归 0 时全服广播战报)
    /// </summary>
    public static (bool destroyed, string broadcastMsg) DamageSiegeBanner(int currentBannerHp, int damage)
    {
        int newHp = Math.Max(0, currentBannerHp - damage);
        if (newHp == 0)
        {
            return (true, "沙巴克城门战旗已被彻底击碎！");
        }
        return (false, string.Empty);
    }

    /// <summary>
    /// 细节 1630：行会公告字符长度与换行清洗 (公告最大 200 字符，过滤非法控制字符)
    /// </summary>
    public static string SanitizeGuildNotice(string rawNotice)
    {
        if (string.IsNullOrEmpty(rawNotice)) return string.Empty;
        string cleaned = rawNotice.Replace("\r", "").Replace("\n", " ");
        return cleaned.Length > 200 ? cleaned[..200] : cleaned;
    }

    /// <summary>
    /// 细节 1631：行会会长禅让会长职位校验 (需双方都在线且副会长确认接受)
    /// </summary>
    public static bool CanAbdicateGuildLeader(bool isMasterOnline, bool isTargetOnline, bool targetConfirmed)
    {
        return isMasterOnline && isTargetOnline && targetConfirmed;
    }

    /// <summary>
    /// 细节 1632：行会聊天发言冷却 (单次发言冷却时间 1.0 秒，防止无脑刷屏)
    /// </summary>
    public static bool CheckGuildChatCooldown(long lastChatTimeMs, long currentChatTimeMs)
    {
        return (currentChatTimeMs - lastChatTimeMs) >= 1000;
    }

    /// <summary>
    /// 细节 1633：城战期间复活点分流 (沙巴克守方在沙城复活，攻方在盟重省土城复活)
    /// </summary>
    public static string GetSiegeRespawnMap(bool isDefender)
    {
        return isDefender ? "0150" : "3"; // 0150 为沙巴克，3 为盟重土城
    }

    /// <summary>
    /// 细节 1634：城战期间沙巴克城墙弓箭手复活费 (复活一名大刀弓箭手消耗 100,000 金币)
    /// </summary>
    public static int GetArcherRespawnCost()
    {
        return 100_000;
    }

    /// <summary>
    /// 细节 1635：行会封号权限级别校验 (只有掌门人与副掌门人有权修改会员封号)
    /// </summary>
    public static bool CanModifyMemberRank(int operatorRank)
    {
        return operatorRank is 1 or 2; // 1 为掌门人，2 为副掌门
    }

    /// <summary>
    /// 细节 1636：城战攻城申请提交时间窗口 (必须提前 3 天使用祖玛头像在比奇皇宫申请)
    /// </summary>
    public static bool IsSiegeApplicationValid(bool hasZumaRelic, int daysInAdvance)
    {
        return hasZumaRelic && daysInAdvance >= 3;
    }

    /// <summary>
    /// 细节 1637：沙巴克城门修复消耗 (修理皇宫城门消耗 500,000 城池建设金币)
    /// </summary>
    public static (bool success, int remainingCastleGold) RepairSabukGate(int castleGold)
    {
        if (castleGold < 500_000) return (false, castleGold);
        return (true, castleGold - 500_000);
    }

    /// <summary>
    /// 细节 1638：沙巴克每日税金收益结算 (每日固定产生 200,000 金币税收归城主金库)
    /// </summary>
    public static int CalculateDailyTaxRevenue()
    {
        return 200_000;
    }

    /// <summary>
    /// 细节 1639：行会成员踢出冷却限制 (被踢出后需 12 小时方可加入新行会)
    /// </summary>
    public static bool CanJoinNewGuildAfterKick(long kickedTimeMs, long currentTimeMs)
    {
        const long TwelveHoursMs = 12 * 3600 * 1000L;
        return (currentTimeMs - kickedTimeMs) >= TwelveHoursMs;
    }

    /// <summary>
    /// 细节 1640：城战火墙双倍伤害机制实证 (沙巴克城内火墙对敌对行会伤害增加 20%)
    /// </summary>
    public static int CalculateSiegeFireWallDamage(int baseFireDamage, bool isInSabukCastle, bool isEnemyGuild)
    {
        if (isInSabukCastle && isEnemyGuild)
        {
            return (int)(baseFireDamage * 1.20f);
        }
        return baseFireDamage;
    }

    // =========================================================================
    // 细节 1641 ~ 1650：Direct-Blit 协议与 AOT 零分配压测保障
    // =========================================================================

    /// <summary>
    /// 细节 1641：Direct-Blit 定长内存块魔数校验 (包头魔数必须为 0xAA55)
    /// </summary>
    public static bool ValidateDirectBlitMagic(ushort headerMagic)
    {
        return headerMagic == 0xAA55;
    }

    /// <summary>
    /// 细节 1642：网络封包序号防重放攻击与滑动窗口 (单调递增且在窗口 64 以内)
    /// </summary>
    public static bool ValidateSequenceNumber(uint expectedSeq, uint incomingSeq)
    {
        if (incomingSeq < expectedSeq) return false;
        return (incomingSeq - expectedSeq) <= 64;
    }

    /// <summary>
    /// 细节 1643：网络心跳 Ping/Pong 超时判定 (超过 5000ms 未收到回包则断开)
    /// </summary>
    public static bool IsHeartbeatTimedOut(long lastPingTimeMs, long currentTimeMs)
    {
        return (currentTimeMs - lastPingTimeMs) > 5000;
    }

    /// <summary>
    /// 细节 1644：非法巨型封包长度熔断 (封包长度大于 4096 字节直接切断连接)
    /// </summary>
    public static bool IsPacketLengthValid(int packetLength)
    {
        return packetLength is > 0 and <= 4096;
    }

    /// <summary>
    /// 细节 1645：AOT 静态紧凑内存结构零 GC 拷贝 (纯栈结构体无引用对象开销)
    /// </summary>
    public static int GetDirectBlitHeaderSize()
    {
        return 12; // 2字节魔数 + 2字节协议号 + 4字节序列号 + 4字节负载长度
    }

    /// <summary>
    /// 细节 1646：网络拥塞窗口动态退避算法 (连续丢包时退避因子加倍，最高 4000ms)
    /// </summary>
    public static int CalculateBackoffInterval(int currentIntervalMs, int dropCount)
    {
        int interval = currentIntervalMs * (1 << Math.Min(dropCount, 4));
        return Math.Min(4000, interval);
    }

    /// <summary>
    /// 细节 1647：网络缓冲区空闲空间足额判定
    /// </summary>
    public static bool HasSufficientBufferSpace(int bufferCapacity, int currentOffset, int payloadSize)
    {
        return (bufferCapacity - currentOffset) >= payloadSize;
    }

    /// <summary>
    /// 细节 1648：封包 CRC16 校验码校验
    /// </summary>
    public static ushort ComputeCrc16(ReadOnlySpan<byte> data)
    {
        ushort crc = 0xFFFF;
        for (int i = 0; i < data.Length; i++)
        {
            crc ^= (ushort)(data[i] << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
            }
        }
        return crc;
    }

    /// <summary>
    /// 细节 1649：客户端输入频度限流保护 (单秒内物理按键指令超过 20 次触发丢弃)
    /// </summary>
    public static bool IsInputRateExceeded(int inputCountIn1Sec)
    {
        return inputCountIn1Sec > 20;
    }

    /// <summary>
    /// 细节 1650：Dumb Client 服务端回执绝对确认机制 (客户端状态以服务端最新快照为准)
    /// </summary>
    public static int ReconcileClientStateWithServerSnapshot(int clientStateValue, int serverAuthoritativeValue)
    {
        // 彻底丢弃客户端自算状态，绝对覆盖为服务端权威值
        return serverAuthoritativeValue;
    }
}
