import random

from math import gcd


def __compute_next(x: int, g: int, y: int, a: int, b: int, p: int, order: int):
    if x % 3 == 0:
        x = (x * x) % p
        a = (2 * a) % order
        b = (2 * b) % order
    elif x % 3 == 1:
        x = (x * y) % p
        b = (b + 1) % order
    else:
        x = (x * g) % p
        a = (a + 1) % order
    return x, a, b


def pollard_rho(g: int, y: int, p: int, order: int = 0, tries: int = 10, verify: bool = False) -> int:
    if verify:
        assert is_prime(p), "Pollard-rho works for prime p"
    else:
        print("WARNING: Pollard-rho running assuming passed n is prime. If not, answer may be wrong.")
    
    if order == 0 or order is None:
        print("WARNING: Order has not been provided to Pollard Rho, answer might not be the smallest one.")
        order = p-1

    if y == 1:
        return 0

    def __pollard_rho(g: int, y: int, p: int, order: int, a0: int, b0: int):

        x0 = (pow(g, a0, p) * pow(y, b0, p)) % p
        xi, ai, bi = x0, a0, b0
        x2i, a2i, b2i = x0, a0, b0

        for i in range(1, p):
            xi, ai, bi = __compute_next(xi, g, y, ai, bi, p, order)
            x2i, a2i, b2i = __compute_next(x2i, g, y, a2i, b2i, p, order)
            x2i, a2i, b2i = __compute_next(x2i, g, y, a2i, b2i, p, order)

            if (xi == x2i):
                beta = (b2i - bi) % order
                alpha = (ai - a2i) % order

                if beta == 0:
                    return -1

                xs = solve_linear_congurence(beta, alpha, order)
                for x in xs:
                    if pow(g, x, p) == y:
                        return x

        return -1

    a0, b0 = 0, 0
    x = __pollard_rho(g, y, p, order, a0, b0)
    if x >= 0:
        return x

    for _ in range(tries):
        a0, b0 = random.randint(1, order), random.randint(1, order)
        x = __pollard_rho(g, y, p, order, a0, b0)
        if x >= 0:
            return x
    
    return x

def mod_inverse(g: int, n: int) -> int:
    
    assert g > 0 and n > 1, "Inappropriate values to compute inverse"

    g = g % n

    _, v, g = extended_euclidean_algorithm(n, g)

    if g != 1:
        print("Inverse doesn't exist")
        exit()
    else:
        v = v % n
        return v


def solve_linear_congurence(a: int, b: int, m: int):

    g = gcd(a, m)
    if b % g != 0:
        print("Recurrence can not be solved")
        return -1

    a, b, m = a//g, b//g, m//g

    a_inverse = mod_inverse(a, m)

    x = (b * a_inverse) % m

    xs = [x + (i*m) for i in range(0, g)]
    return xs

def is_prime(n: int) -> bool:
    assert n > 1, "Вхідний параметр повинен бути > 1"

    if n in [2, 3, 5, 7]:
        return True
    if n % 2 == 0 or n % 3 == 0:
        return False
    upper_bound = ceil(n ** 0.5)
    divisor = 5
    while (divisor <= upper_bound):
        if n % divisor == 0 or n % (divisor +2) == 0:
            return False
        divisor += 6
    return True

def extended_euclidean_algorithm(a: int, b: int) -> tuple[int, int, int]:
    s, old_s = 0, 1
    t, old_t = 1, 0

    r, old_r = b, a

    while (r != 0):

        q = old_r // r

        old_r, r = r, old_r % r

        old_s, s = s, old_s - q * s
        old_t, t = t, old_t - q * t

    return old_s, old_t, old_r

def main():
    y, g, p = 2, 10, 19
    x = pollard_rho(g,y,p)
    print(x % p-1)

if __name__== "__main__":
    main()
