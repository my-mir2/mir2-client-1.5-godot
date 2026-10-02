import random

def test_weapon_drop(trials=1000000):
    plus_1 = 0
    plus_2 = 0
    plus_3 = 0
    plus_7 = 0
    
    for _ in range(trials):
        if random.randint(0, 14) != 0: # 1/15 enter
            continue
            
        points = 0
        for _ in range(12):
            if random.randint(0, 14) != 0:
                break
            points += 1
            
        if random.randint(0, 14) == 0:
            val = points + 1
            if val == 1: plus_1 += 1
            elif val == 2: plus_2 += 1
            elif val == 3: plus_3 += 1
            elif val == 7: plus_7 += 1
            
    print(f"Total Trials: {trials}")
    print(f"+1: {plus_1} ({(plus_1/trials)*100:.6f}%) -> 1 in {trials/max(1, plus_1):.0f}")
    print(f"+2: {plus_2} ({(plus_2/trials)*100:.6f}%) -> 1 in {trials/max(1, plus_2):.0f}")
    print(f"+3: {plus_3} ({(plus_3/trials)*100:.6f}%) -> 1 in {trials/max(1, plus_3):.0f}")
    print(f"+7: {plus_7} ({(plus_7/trials)*100:.6f}%) -> 1 in {trials/max(1, plus_7):.0f}")

test_weapon_drop(10000000)
