from math import ceil, sqrt

def bsgs(g, y, p, verify):
    """
    Алгоритм Гельфорда-Шенкса
    :param g: g з рівняння g^x = y (mod n). g > 0.
    :param y: y з рівняння g^x = y (mod n). Не може бути від'ємним.
    :param p: p з рівняння g^x = y (mod p). Просте числе.
    :returns: x з рівняння g^x = y (mod n). Якщо такого числа не знайдено, повертає 1.
    """

    if verify:
        assert is_prime(p), "p має бути простим числом"
    else:
        print("Алгоритм виконується з припущенням що p - просте число.")
    m = ceil(sqrt(p))

    # В даній хешмапі зберігаємо обчислені p^j
    table = dict()
    # Заповнюємо хешмапу та визначаємо всі g^j
    g_raised_to_j = 1
    for j in range(0, m):
        table[g_raised_to_j] = j
        g_raised_to_j = (g_raised_to_j * g) % p
    
    print(table)
    #Маємо визначити g^(-im)
    # для цього спочатку визначаємо g^(-m) та підносимо його до степеню в циклі
    g_raised_to_minus_m = pow(g, p-(m+1), p)
    # змінна зберігає g^x * g^(-im)
    temp = y
    for i in range(0, m):
        if temp in table:
            return (i * m) + table[temp]
        temp = (temp * g_raised_to_minus_m) % p
    return -1

def is_prime(n: int) -> bool:
    """
    Перевіряє, чи є n простим числом
    :param n: ціле числе > 1.
    :returns: true якщо n просте число, інакше false
    """
    assert n > 1, "Вхідний параметр повинен бути > 1"

    if n in [2, 3, 5, 7]:
        # n - просте число
        return True
    if n % 2 == 0 or n % 3 == 0:
        # має дільником 2 або 3
        return False
    # sqrt(n) є верхньою межею для дільника
    upper_bound = ceil(n ** 0.5)
    divisor = 5
    # кожне просте число окрім 2 та 3
    # має вигляд 6k +- 1
    # тому починаємо з 5 та збільшуємо дільник на 6
    while (divisor <= upper_bound):
        if n % divisor == 0 or n % (divisor +2) == 0:
            return False
        divisor += 6
    return True

def main():
    y, g, p = 2, 10, 19    
    x = bsgs(y, g, p, True)
    print(x % p-1)

if __name__== "__main__":
  main()