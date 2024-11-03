# -*- coding: utf-8 -*-

import math
import cmath

## constants
h  = 0.0
pi = 3.141592653589793238462643383279502
e  = 2.718281828459045235360287471352662
J  = 1j
# multiple prefixes
Y = 10.0 ** 24  # Yotta
Z = 10.0 ** 21  # Zetta
E = 10.0 ** 18  # Ecsa
P = 10.0 ** 15  # Peta
T = 10.0 ** 12  # Tera
G = 10.0 ** 9   # Giga
M = 10.0 ** 6   # Mega
k = 10.0 ** 3   # kilo
# submultipliers
m = 10.0 ** -3  # milli
u = 10.0 ** -6  # micro
n = 10.0 ** -9  # nano
p = 10.0 ** -12 # pico
f = 10.0 ** -15 # femto
a = 10.0 ** -18 # atto
z = 10.0 ** -21 # zetto
y = 10.0 ** -24 # yocto
# prefixes list
prefixes =['Y','Z','E','P','T','G','M','k','m','u','n','p','f','a','z','y','J']

## functions
def sqrt(x):
    if type(x) is complex:
        return cmath.sqrt(x)
    return math.sqrt(x)

def root(x,n):
    if type(n) is int:
        if x < 0 and (n % 2) == 1:
            return -abs(x) ** (1.0 / n)
    return x ** (1.0 / n)

def log(x,n):
    if type(x) is complex:
        return cmath.log(x,n)
    return math.log(x,n)

def ln(x):
    if type(x) is complex:
        return cmath.log(x)
    return math.log(x)

def lg(x):
    if type(x) is complex:
        return cmath.log10(x)
    return math.log10(x)

def exp(x):
    if type(x) is complex:
        return cmath.exp(x)
    return math.exp(x)

def sign(x):
    if x < 0.0:
        return -1
    if x > 0.0:
        return 1
    return 0

def fact(x):
    return math.factorial(x)

# complex functions
def re(x):
    if type(x) is complex:
        return x.real
    return x

def im(x):
    if type(x) is complex:
        return x.imag
    return 0.0

def arg(x):
    if type(x) is complex:
        return cmath.phase(x)
    if x < 0.0:
        return pi
    return 0.0

# trigonometric functions
def sin(x):
    if type(x) is complex:
        return cmath.sin(x)
    return math.sin(x)

def cos(x):
    if type(x) is complex:
        return cmath.cos(x)
    return math.cos(x)

def tan(x):
    if type(x) is complex:
        return cmath.tan(x)
    return math.tan(x)

def csc(x):
    if type(x) is complex:
        return 1.0/cmath.sin(x)
    return 1.0/math.sin(x)

def sec(x):
    if type(x) is complex:
        return 1.0/cmath.cos(x)
    return 1.0/math.cos(x)

def cot(x):
    if type(x) is complex:
        return 1.0/cmath.tan(x)
    return 1.0/math.tan(x)

# inverse trigonometric functions
def asin(x):
    if type(x) is complex:
        return cmath.asin(x)
    return math.asin(x)

def acos(x):
    if type(x) is complex:
        return cmath.acos(x)
    return math.acos(x)

def atan(x):
    if type(x) is complex:
        return cmath.atan(x)
    return math.atan(x)

def acsc(x):
    if type(x) is complex:
        return cmath.asin(1.0/x)
    return math.asin(1.0/x)

def asec(x):
    if type(x) is complex:
        return cmath.acos(1.0/x)
    return math.acos(1.0/x)

def acot(x):
    if type(x) is complex:
        return cmath.atan(1.0/x)
    return math.atan(1.0/x)

# hyperbolic functions
def sh(x):
    if type(x) is complex:
        return cmath.sinh(x)
    return math.sinh(x)

def ch(x):
    if type(x) is complex:
        return cmath.cosh(x)
    return math.cosh(x)

def th(x):
    if type(x) is complex:
        return cmath.tanh(x)
    return math.tanh(x)

def csch(x):
    if type(x) is complex:
        return 1.0/cmath.cosh(x)
    return 1.0/math.cosh(x)

def sech(x):
    if type(x) is complex:
        return 1.0/cmath.sinh(x)
    return 1.0/math.sinh(x)

def cth(x):
    if type(x) is complex:
        return 1.0/cmath.tanh(x)
    return 1.0/math.tanh(x)

# inverse hyperbolic functions
def ash(x):
    if type(x) is complex:
        return cmath.asinh(x)
    return math.asinh(x)

def ach(x):
    if type(x) is complex:
        return cmath.acosh(x)
    return math.acosh(x)

def ath(x):
    if type(x) is complex:
        return cmath.atanh(x)
    return math.atanh(x)

def acsch(x):
    if type(x) is complex:
        return cmath.asinh(1.0/x)
    return math.asinh(1.0/x)

def asech(x):
    if type(x) is complex:
        return cmath.acosh(1.0/x)
    return math.acosh(1.0/x)

def acth(x):
    if type(x) is complex:
        return cmath.atanh(1.0/x)
    return math.atanh(1.0/x)

# angle conversion functions
def rad(x):
    return math.radians(x)

def deg(x):
    return math.degrees(x)

def rtd(x):
    return x/pi*180.0

def rtg(x):
    return x/pi*200.0

def dtr(x):
    return x/180.0*pi

def dtg(x):
    return x/180.0*200

def gtr(x):
    return x/200.0*pi

def gtd(x):
    return x/200.0*180

# finds prefix f in string x
def fnd(x,f):
    for i in range(1,len(x)):
        if (x[i] == f) and (x[i-1].isdigit()):
            return i
    return None

# find start of coeff. with pos. n
def strt(x,n):
    for i in range(n-1,0,-1):
        if not (x[i] in ['.','0','1','2','3','4','5','6','7','8','9']):
            return i+1
    return 0

# parse prefixes 1.05m -> (1.05*m)
def parse(x):
    for p in prefixes:
        pos = fnd(x,p)
        while pos != None:
            st = strt(x,pos)
            x = x[:st]+'('+x[st:pos]+'*'+x[pos]+')'+x[pos+1:]
            pos = fnd(x,p)
    return x

# formatting to string exclude exp
def un_exp(x):
    dig = str(x)
    if dig.find('e') != -1:
        img = dig.split('e')
        dig = img[0]
        pwr = eval(img[1].replace('0',''))
        if pwr < 0:
            dig = '0.'+'0'*abs(pwr+1)+dig.replace('.','')
        else:
            if dig.find('.') != -1:
                pwr -= len(dig.split('.')[1])
                dig = dig.replace('.','')
            dig += '0'*pwr
    return dig

# formatting 1-999+prefix
def prefixed(x):
    result = ''
    _prefixes = []
    _prefix = 0
    if abs(x) < 1.0:
        _prefixes = ['m','u','n','p','f','a','z','y']
        while abs(x) < 1.0:
            x *= 1000.0
            _prefix += 1
    elif abs(x) > 999.0:
        _prefixes = ['k','M','G','T','P','E','Z','Y']
        while abs(x) > 999.0:
            x /= 1000.0
            _prefix += 1
    result = str(x)
    if _prefix > 0:
        p = _prefixes[_prefix-1]
        if result.find('.') != -1:
            result = result.replace('.',p)
            result = result.rstrip('0')
        else:
            result += p
    return result

# calculate
def calculate(expression,formats):
    t = 0.0
    try:
        t = eval(parse(expression))
    except:
        t = None
    if t == None:
        return 'Error'
    else:
        global h
        h = t
        if formats == 1:
            return un_exp(h)
        elif formats == 2:
            return prefixed(h)
        else:
            return str(h)