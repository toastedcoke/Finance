# Net Present Value Calculator

This application will calculate the Net Present Value for a series of cashflows given the lower and upper bound rate with the given incremental rate per year.

Below is the formula used to compute the NPV:
<img class="aligncenter wp-image-8349" src="https://cdn.corporatefinanceinstitute.com/assets/net-present-value3.png" alt="Net Present Value (NPV) Formula" width="636" height="98" srcset="https://cdn.corporatefinanceinstitute.com/assets/net-present-value3.png 442w, /assets/net-present-value3-300x46.png 300w, /assets/net-present-value3-24x4.png 24w, /assets/net-present-value3-36x6.png 36w, /assets/net-present-value3-48x7.png 48w" sizes="(max-width: 636px) 100vw, 636px">

##### Where:

- **Z<sub>1</sub>** = Cash flow in time 1
- **Z<sub>2</sub>** = Cash flow in time 2
- **r** = Discount rate
- **X<sub>0</sub>**  = Cash outflow in time 0 (i.e. the purchase price / initial investment)

**Note:**
- The initial value for the discount rate in first year is the lower rate, succeeding year is lower rate plus the sum of incemental rate up to year *N*.
- The maximum year is up to the Upper Bound rate
