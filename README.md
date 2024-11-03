# IronCalc

A simple engineering calculator with support for complex numbers and various number systems.

![screen1](https://github.com/user-attachments/assets/091c70c2-bc45-422b-a027-9b50a02ae1f6)
![screen2](https://github.com/user-attachments/assets/f4f67a7f-6a4c-4776-955d-cc079386e087)
![screen3](https://github.com/user-attachments/assets/6bd660a0-35c8-4290-a390-b5150bd993df)
![screen4](https://github.com/user-attachments/assets/30914d63-5e5d-4cd1-bca0-dfa2dc0334ec)

## Documentation

The expression value calculation is implemented using IronPython, which explains the following features:
  * The expression is not limited to one mathematical operation `2+3*4*(5+6*7)-sqrt(2)`
  * If at least one operand is a real number, the result of the expression will be real `2/3=0` and `2./3=0.666666666667`
  * Special variable `_` - stores the result of the last calculation
  * The implementation of mathematical functions is in the `calc.py` file and can be supplemented or changed as needed.

Tabs:
* Regular
  | Operator               | Example                                                                |
  |------------------------|------------------------------------------------------------------------|
  | addition               | `1+2`                                                                  |
  | subtraction            | `1-2`                                                                  |
  | multiplication         | `1×2`, `1*2`                                                           |
  | division               | `11/2=5`, `11./2=5.5`                                                  |
  | integer division       | `11//2=5`, `11.//2=5.0`                                                |
  | modulo                 | `11%2=1`, `11.%2=1.0`                                                  |
  | square `x²`            | `11²`, `11^2`, `11××2`, `11**2`, `pow(11,2)`                           |
  | square root `√x`       | `sqrt(10)`, `10^.5`, `pow(10,.5)`                                      |
  | power `xⁿ`             | `2^3`, `2××3`, `2**3`, `pow(2,3)`                                      |
  | root `ⁿ√x`             | `root(8,3)`, `8^(1./3)`, `pow(8,1./3)`                                 |
  | exponent `℮ˣ`          | `exp(2)`, `℮^2`, `pow(℮,2)`                                            |
  | natural logarithm `ln` | `ln(5)`, `log(5,℮)`                                                    |
  | common logarithm `lg`  | `lg(5)`, `log(5,10)`                                                   |
  | logarithm `log`        | `log(8,2)`                                                             |
  | real part `re`         | `re(1)=1`, `re(1+j)=1.0`, `re(j)=0.0`                                  |
  | imaginary part `im`    | `im(1)=0.0`, `im(1+j)=1.0`, `im(j)=1.0`                                |
  | argument `arg`         | `arg(1)=0.0`, `arg(-1)=π`, `arg(1+j)=π/4`, `arg(j)=π/2`, `arg(-j)=-π/2`|
  | imaginary unit `j`     | `1+j`, `2+3j`, `2+3×j`, `2+3*j`                                        |
* Trigonometry
   - Trigonometric functions `sin`, `cos`, `tan`, `csc`, `sec`, `cot`
   - Inverse trigonometric functions `asin`, `acos`, `atan`, `acsc`, `asec`, `acot`
   - Hyperbolic functions `sh`, `ch`, `th`, `csch`, `sech`, `cth`
   - Inverse hyperbolic functions `ash`, `ach`, `ath`, `acsch`, `asech`, `acth`
   - Angle measure conversion functions
     | Description                   | Sysntax |
     |-------------------------------|---------|
     | radians to degrees  `rad→deg` | `rtd()` |
     | radians to gradians `rad→grd` | `rtg()` |
     | degrees to radians  `deg→rad` | `dtr()` |
     | degrees to gradians `deg→grd` | `dtg()` |
     | gradians to radians `grd→rad` | `gtr()` |
     | gradians to degrees `grd→deg` | `gtd()` |
* Constants
   - basic math constatnts `π`, `℮`
   - submultipliers
     | Description | Prefix | Meaning           |
     |-------------|--------|-------------------|
     | milli       | `m`    | `10^-3`, `1e-3`   |
     | micro       | `u`    | `10^-6`, `1e-6`   |
     | nano        | `n`    | `10^-9`, `1e-9`   |
     | pico        | `p`    | `10^-12`, `1e-12` |
     | femto       | `f`    | `10^-15`, `1e-15` |
   - multiple prefixes
     | Description | Prefix | Meaning         |
     |-------------|--------|-----------------|
     | kilo        | `k`    | `10^3`, `1e3`   |
     | mega        | `M`    | `10^6`, `1e6`   |
     | giga        | `G`    | `10^9`, `1e9`   |
     | tera        | `T`    | `10^12`, `1e12` |
     | peta        | `P`    | `10^15`, `1e15` |
* SystemValue
   - four fixed bases `bin`, `oct`, `dec`, `hex`
   - arbitrary base from 2 to 16
   - four variants of integer bit depth `8`, `16`, `32`, `64`
   - bitwise operations
     | Description                        | Sysntax |
     |------------------------------------|---------|
     | convertion to two`s complenet code | `neg`   |
     | bitwise not                        | `~`     |
     | bitwise or                         | `\|`    |
     | bitwise xor                        | `^`     |
     | bitwise and                        | `&`     |
