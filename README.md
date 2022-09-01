# FFTBench
Compares different FFT implementations for .Net programs.

Thanks to Christian Woltering and his arcticle "Comparison of FFT Implementations for .NET" [https://www.codeproject.com/articles/1095473/comparison-of-fft-implementations-for-net] which is the base for this project.

Further thanks to PeterPet for his FFTS project [https://github.com/PetterPet01/FFTSSharp], and Chris Lomont for providing his implementation.

The following FFT implementations can be compared:
-	Accord (https://github.com/accord-net)
-	AForge (http://www.aforgenet.com/framework/)
-	DSPLib (http://en.dsplib.org/)
-	Exocortex (https://benhouston3d.com/dsp/)
-	Math.NET (https://numerics.mathdotnet.com/)
-	NAudio (https://github.com/naudio/NAudio)
-	Lomont (https://lomont.org/software/misc/fft/LomontFFT.html)
-	FFTW (https://fftw.org/)
-	FFTS (https://github.com/anthonix/ffts) (https://github.com/PetterPet01/FFTSSharp)
-	Intel® oneAPI Math Kernel Library (https://www.intel.com/content/www/us/en/developer/tools/oneapi/onemkl.html#gs.avy43w) [MKL]

## Usage
Open the solution, compile and start the FFTBench project.

## Explanation

The name of each FFT implementation might be extended with one or more of the following:
- "stretched" means that the input data is extended so that it fits into an array whose size is a power of two.
- "real" means that a FFT of only real numbers was performed. If this is not part of a name (mostly) a FFT of complex numbers is performed.
- "32" means that only single precision number were used for the FFT. If this is not part of a name (mostly) double precision number were used.
- "inplace" means that the input overwrites the output. More about this can be found here: https://www.intel.com/content/www/us/en/develop/documentation/onemkl-developer-reference-c/top/fourier-transform-functions/fft-functions/configuration-settings/dfti-placement.html

For more details please see the well written artice of Christian Woltering: https://www.codeproject.com/articles/1095473/comparison-of-fft-implementations-for-net

## Disclaimer
FFTW (real) might not be implemented correctly.
Only runs on Windows.

## Results

Here are some results.
![Capture](https://user-images.githubusercontent.com/5380109/187858219-bebac7d5-dd43-4918-a09d-f07f0de7f236.PNG)
Figure 1: Results for different lengths of input arrays ("Size") and 1000 FFT's performed on each array.

Table 1: Results on OS=Microsoft Windows NT 6.2.9200.0, Processor=11th Gen Intel(R) Core(TM) i7-11800H @ 2.30GHz, ProcessorCount=16, CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE ([results.pdf](https://github.com/TSerious/FFTBench/files/9468389/results.pdf))
| ||Size| | |Size| | |Size| | |Size| | |Size| | |Size| | |Size| | |
|:----|-|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|
| ||256| | |512| | |1024| | |2048| | |4096| | |8192| | |16384| | |
|Name||Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|Total [ms]|Average [ms]|Average [ticks]|
|Accord||530|0,01|106,15|1097|0,02|219,55|2267|0,05|453,50|4637|0,09|927,56|9477|0,19|1895,56|19602|0,39|3920,45|40190|0,80|8038,14|
|AForge||149|0,00|30,00|327|0,01|65,49|699|0,01|139,89|1551|0,03|310,28|4065|0,08|813,03|9045|0,18|1809,18|22624|0,45|4525,00|
|Math.NET||25|0,00|5,08|42|0,00|8,48|93|0,00|18,64|245|0,00|49,17|536|0,01|107,39|1716|0,03|343,36|3153|0,06|630,67|
|NAudio||146|0,00|29,20|288|0,01|57,79|596|0,01|119,27|1299|0,03|259,83|2900|0,06|580,02|7372|0,15|1474,55|17590|0,35|3518,01|
|DSPLib||200|0,00|40,05|419|0,01|83,84|917|0,02|183,45|1848|0,04|369,78|4026|0,08|805,26|8907|0,18|1781,45|19400|0,39|3880,15|
|Lomont||143|0,00|28,65|288|0,01|57,65|599|0,01|119,89|1319|0,03|263,99|3314|0,07|662,88|7771|0,16|1554,27|19206|0,38|3841,35|
|Lomont (real)||77|0,00|15,53|162|0,00|32,50|338|0,01|67,79|724|0,01|144,85|1586|0,03|317,22|4061|0,08|812,40|8834|0,18|1766,96|
|Exocortex||118|0,00|23,61|268|0,01|53,76|588|0,01|117,73|1304|0,03|260,99|3642|0,07|728,44|7979|0,16|1595,93|20945|0,42|4189,19|
|Exocortex (real32)||65|0,00|13,11|141|0,00|28,29|320|0,01|64,20|679|0,01|135,81|1445|0,03|289,13|3110|0,06|622,14|7822|0,16|1564,50|
|FFTS_32||17|0,00|3,42|33|0,00|6,75|73|0,00|14,74|172|0,00|34,44|376|0,01|75,39|802|0,02|160,58|1793|0,04|358,70|
|FFTS_32(real)||11|0,00|2,32|23|0,00|4,72|46|0,00|9,36|104|0,00|20,95|221|0,00|44,34|487|0,01|97,51|1013|0,02|202,63|
|FFTW||12|0,00|2,49|27|0,00|5,52|64|0,00|12,82|145|0,00|29,09|394|0,01|78,82|909|0,02|181,81|1976|0,04|395,29|
|FFTW (real)||15|0,00|3,03|35|0,00|7,18|47|0,00|9,40|138|0,00|27,64|229|0,00|45,89|681|0,01|136,26|1561|0,03|312,31|
|MKL (real)||8|0,00|1,65|16|0,00|3,29|31|0,00|6,34|93|0,00|18,73|212|0,00|42,47|440|0,01|88,02|1069|0,02|213,88|
|MKL (real,inplace)||8|0,00|1,71|16|0,00|3,34|32|0,00|6,47|77|0,00|15,42|203|0,00|40,75|439|0,01|87,94|1071|0,02|214,23|
|MKL (real32)||5|0,00|1,02|9|0,00|1,85|17|0,00|3,52|37|0,00|7,42|92|0,00|18,48|218|0,00|43,74|449|0,01|89,93|
|MKL||27|0,00|5,53|47|0,00|9,60|109|0,00|21,88|590|0,01|118,07|1073|0,02|214,73|1736|0,03|347,22|3278|0,07|655,74|
|MKL32||28|0,00|5,80|33|0,00|6,61|65|0,00|13,01|449|0,01|89,95|702|0,01|140,49|1073|0,02|214,61|1898|0,04|379,73|

## Conclusion

The FFT libraries with the best performance are FFTS, FFTW and MKL (Intel®). Depending on what data you want to process chosing the right library might at least double the performance.

## License
This project is licensed under the MIT license but certain parts of it (for example FFTW) are published under a different license!
Please be aware of this and check if you are using the correct license for your project.
