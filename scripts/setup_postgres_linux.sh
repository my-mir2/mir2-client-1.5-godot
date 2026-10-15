#!/usr/bin/env bash
# ==============================================================================
# 盛大热血传奇 1.5 官方原版 Linux 云端 (Google Jules) 数据库与服务端一键初始化脚本
# 自动部署 PostgreSQL、创建 mir2_world 数据库、导入官方全量地图/怪物/刷怪/掉落数据
# ==============================================================================
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"

echo "=== [1/4] 检测与安装 PostgreSQL 服务 ==="
if ! command -v psql &> /dev/null; then
    echo "未检测到 psql，正在尝试通过 apt-get 安装 postgresql..."
    sudo apt-get update
    sudo apt-get install -y postgresql postgresql-contrib
fi

echo "=== [2/4] 启动 PostgreSQL 服务并配置本地密码 ==="
sudo service postgresql start || true

# 确保 postgres 用户密码为 postgres，支持 127.0.0.1 信任/密码连接
sudo -u postgres psql -c "ALTER USER postgres WITH PASSWORD 'postgres';"
sudo -u postgres psql -c "CREATE DATABASE mir2_world OWNER postgres;" 2>/dev/null || true

echo "=== [3/4] 恢复官方 1.4/1.5 数据库全量大盘数据 ==="
DATA_GZ="${PROJECT_ROOT}/data/official_14/mir2_world_official_data.sql.gz"
DATA_SQL="${PROJECT_ROOT}/data/official_14/official_14_db.sql"

if [ -f "${DATA_GZ}" ]; then
    echo "正在从压缩归档 ${DATA_GZ} 导入官方 475 张地图、2346 条刷怪、17333 条掉落规则..."
    gunzip -c "${DATA_GZ}" | PGPASSWORD=postgres psql -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -q
    echo "官方大盘数据导入完成！"
elif [ -f "${DATA_SQL}" ]; then
    echo "正在从 ${DATA_SQL} 导入基础数据..."
    PGPASSWORD=postgres psql -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -f "${DATA_SQL}" -q
fi

echo "=== [4/4] 验证数据库表与记录数 ==="
PGPASSWORD=postgres psql -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c "\dt"
PGPASSWORD=postgres psql -h 127.0.0.1 -p 5432 -U postgres -d mir2_world -c "SELECT 'official_map' tbl, count(*) FROM official_map UNION ALL SELECT 'official_monster', count(*) FROM official_monster UNION ALL SELECT 'official_mongen', count(*) FROM official_mongen UNION ALL SELECT 'official_mon_drop', count(*) FROM official_mon_drop UNION ALL SELECT 'official_npc_placement', count(*) FROM official_npc_placement;"

echo "=============================================================================="
echo " PostgreSQL 工业级数据库物理就绪 100%！"
echo " 接下来可直接执行: dotnet run --project src/Mir2.Server/Mir2.Server.csproj"
echo "=============================================================================="
