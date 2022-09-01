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
-	Intel® oneAPI Math Kernel Library (https://www.intel.com/content/www/us/en/developer/tools/oneapi/onemkl.html#gs.avy43w)

## Usage
Open the solution, compile and start the FFTBench project.

## Explanation
For more details please see the well written artice of Christian Woltering: https://www.codeproject.com/articles/1095473/comparison-of-fft-implementations-for-net

## Disclaimer
FFTW (real) might not be implemented correctly.
Only runs on Windows.

## Results

Here are some results.
![Capture](https://user-images.githubusercontent.com/5380109/187858219-bebac7d5-dd43-4918-a09d-f07f0de7f236.PNG)

Explanation:
- "real" means that a FFT of only real numbers was performed. If this is not part of a name (mostly) a FFT of complex numbers is performed.
- "32" means that only single precision number were used for the FFT. If this is not part of a name (mostly) double precision number were used.
- "inplace" means that the input overwrites the output. More about this can be found here: https://www.intel.com/content/www/us/en/develop/documentation/onemkl-developer-reference-c/top/fourier-transform-functions/fft-functions/configuration-settings/dfti-placement.html



## License
This project is licensed under the MIT license but certain parts of it (for example FFTW) are published under a different license!
Please be aware of this and check if you are using the correct license for your project.
