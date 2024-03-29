using System;

/*
This class was translated from Dr. Kostadinov's work, which in turn translated 
the original FORTRAN code provided at the NASA GISS website http://aom.giss.nasa.gov/
specific link no longer available as of September 2013.
The original FORTRAN code is due to Gary L. Russell and is provided here
as comments for reference. 

The algorithm itself is provided by Berger (1978) - see references below. 
The source of these calculations is: Andre L. Berger,
        /// 1978, "Long-Term Variations of Daily Insolation and Quaternary
        /// Climatic Changes", JAS, v.35, p.2362.  Also useful is: Andre L.
        /// Berger, May 1978, "A Simple Algorithm to Compute Long Term
        /// Variations of Daily Insolation", published by Institut
        /// D'Astronomie de Geophysique, Universite Catholique de Louvain,
        /// Louvain-la Neuve, No. 18.
        /// Tables and equations refer to the first reference (JAS).  The
        /// corresponding table or equation in the second reference is
        /// enclosed in parentheses.  The coefficients used in this
        /// subroutine are slightly more precise than those used in either
        /// of the references.  The generated orbital parameters are precise
        /// within plus or minus 1000000 years from present.
*/


namespace Bergers{
    /// <summary>
    /// Class <c>BergerSol</c> Computes Berger's Solution to find Milankovitch parameters
    /// </summary>
    public class BergerSol
    {
        //double eccen, obliq, omega_bar;
        
        /// <summary>
        /// <para>
        /// This method calculates the three Milankovitch parameters as a 
        /// function of YEAR. 
        /// </para>
        /// </summary>
        /// <param name="yr">years A.D. are positive, B.C. are negative</param>
        /// <param name="eccen">eccentricity of orbital ellipse (dimensionless)</param>
        /// <param name="obliq">latitude of Tropic of Cancer in degrees</param>
        /// <param name="omega_bar">
        /// longitude of perihelion = spatial angle from vernal equinox to perihelion in degrees with sun as angle vertex
        /// </param>
        public static void CalculateOrbitalParameters(int yr, out double eccen, out double obliq, out double omega_bar)
        {

            // TABLE1(3,47),TABLE4(3,19),TABLE5(3,78)
            // Table 1 (2).  Obliquity relative to mean ecliptic of date: OBLIQD
            double[,] t1 = new double[,] {
                {-2462.2214466, 31.609974, 251.9025},
                {-857.3232075, 32.620504, 280.8325},
                {-629.3231835, 24.172203, 128.3057},
                {-414.2804924, 31.983787, 292.7252},
                {-311.7632587, 44.828336, 15.3747},
                {308.9408604, 30.973257, 263.7951},
                {-162.5533601, 43.668246, 308.4258},
                {-116.1077911, 32.246691, 240.0099},
                {101.1189923, 30.599444, 222.9725},
                {-67.6856209, 42.681324, 268.7809},
                {24.9079067, 43.836462, 316.7998},
                {22.5811241, 47.439436, 319.6024},
                {-21.1648355, 63.219948, 143.8050},
                {-15.6549876, 64.230478, 172.7351},
                {15.3936813, 1.010530, 28.9300},
                {14.6660938, 7.437771, 123.5968},
                {-11.7273029, 55.782177, 20.2082},
                {10.2742696, 0.373813, 40.8226},
                {6.4914588, 13.218362, 123.4722},
                {5.8539148, 62.583231, 155.6977},
                {-5.4872205, 63.593761, 184.6277},
                {-5.4290191, 76.438310, 267.2772},
                {5.1609570, 45.815258, 55.0196},
                {5.0786314, 8.448301, 152.5268},
                {-4.0735782, 56.792707, 49.1382},
                {3.7227167, 49.747842, 204.6609},
                {3.3971932, 12.058272, 56.5233},
                {-2.8347004, 75.278220, 200.3284},
                {-2.6550721, 65.241008, 201.6651},
                {-2.5717867, 64.604291, 213.5577},
                {-2.4712188, 1.647247, 17.0374},
                {2.4625410, 7.811584, 164.4194},
                {2.2464112, 12.207832, 94.5422},
                {-2.0755511, 63.856665, 131.9124},
                {-1.9713669, 56.155990, 61.0309},
                {-1.8813061, 77.448840, 296.2073},
                {-1.8468785, 6.801054, 135.4894},
                {1.8186742, 62.209418, 114.8750},
                {1.7601888, 20.656133, 247.0691},
                {-1.5428851, 48.344406, 256.6114},
                {1.4738838, 55.145460, 32.1008},
                {-1.4593669, 69.000539, 143.6804},
                {1.4192259, 11.071350, 16.8784},
                {-1.1818980, 74.291298, 160.6835},
                {1.1756474, 11.047742, 27.5932},
                {-1.1316126, 0.636717, 348.1074},
                {1.0896928, 12.844549, 82.6496}
            };

            // Table 4 (1).  Fundamental elements of the ecliptic: ECCEN sin(pi)
            double[,] t4 = new double[,]
            {
                { 0.01860798,  4.207205,  28.620089},
                { 0.01627522,  7.346091, 193.788772},
                {-0.01300660, 17.857263, 308.307024},
                { 0.00988829, 17.220546, 320.199637},
                {-0.00336700, 16.846733, 279.376984},
                { 0.00333077,  5.199079,  87.195000},
                {-0.00235400, 18.231076, 349.129677},
                { 0.00140015, 26.216758, 128.443387},
                { 0.00100700,  6.359169, 154.143880},
                { 0.00085700, 16.210016, 291.269597},
                { 0.00064990,  3.065181, 114.860583},
                { 0.00059900, 16.583829, 332.092251},
                { 0.00037800, 18.493980, 296.414411},
                {-0.00033700,  6.190953, 145.769910},
                { 0.00027600, 18.867793, 337.237063},
                { 0.00018200, 17.425567, 152.092288},
                {-0.00017400,  6.186001, 126.839891},
                {-0.00012400, 18.417441, 210.667199},
                { 0.00001250,  0.667863,  72.108838}
            };

            // Table 5 (3).  General precession in longitude: psi
            double[,] t5 = new double[,]
            {
                { 7391.0225890, 31.609974, 251.9025},
                { 2555.1526947, 32.620504, 280.8325},
                { 2022.7629188, 24.172203, 128.3057},
                {-1973.6517951,  0.636717, 348.1074},
                { 1240.2321818, 31.983787, 292.7252},
                { 953.8679112,  3.138886, 165.1686},
                {-931.7537108, 30.973257, 263.7951},
                { 872.3795383, 44.828336,  15.3747},
                { 606.3544732,  0.991874,  58.5749},
                {-496.0274038,  0.373813,  40.8226},
                { 456.9608039, 43.668246, 308.4258},
                { 346.9462320, 32.246691, 240.0099},
                {-305.8412902, 30.599444, 222.9725},
                { 249.6173246,  2.147012, 106.5937},
                {-199.1027200, 10.511172, 114.5182},
                { 191.0560889, 42.681324, 268.7809},
                {-175.2936572, 13.650058, 279.6869},
                { 165.9068833,  0.986922,  39.6448},
                { 161.1285917,  9.874455, 126.4108},
                { 139.7878093, 13.013341, 291.5795},
                {-133.5228399,  0.262904, 307.2848},
                { 117.0673811,  0.004952,  18.9300},
                { 104.6907281,  1.142024, 273.7596},
                { 95.3227476, 63.219948, 143.8050},
                { 86.7824524,  0.205021, 191.8927},
                { 86.0857729,  2.151964, 125.5237},
                { 70.5893698, 64.230478, 172.7351},
                {-69.9719343, 43.836462, 316.7998},
                {-62.5817473, 47.439436, 319.6024},
                { 61.5450059,  1.384343,  69.7526},
                {-57.9364011,  7.437771, 123.5968},
                { 57.1899832, 18.829299, 217.6432},
                {-57.0236109,  9.500642,  85.5882},
                {-54.2119253,  0.431696, 156.2147},
                { 53.2834147,  1.160090,  66.9489},
                { 52.1223575, 55.782177,  20.2082},
                {-49.0059908, 12.639528, 250.7568},
                {-48.3118757,  1.155138,  48.0188},
                {-45.4191685,  0.168216,   8.3739},
                {-42.2357920,  1.647247,  17.0374},
                {-34.7971099, 10.884985, 155.3409},
                { 34.4623613,  5.610937,  94.1709},
                {-33.8356643, 12.658184, 221.1120},
                { 33.6689362,  1.010530,  28.9300},
                {-31.2521586,  1.983748, 117.1498},
                {-30.8798701, 14.023871, 320.5095},
                { 28.4640769,  0.560178, 262.3602},
                {-27.1960802,  1.273434, 336.2148},
                { 27.0860736, 12.021467, 233.0046},
                {-26.3437456, 62.583231, 155.6977},
                { 24.7253740, 63.593761, 184.6277},
                { 24.6732126, 76.438310, 267.2772},
                { 24.4272733,  4.280910,  78.9281},
                { 24.0127327, 13.218362, 123.4722},
                { 21.7150294, 17.818769, 188.7132},
                {-21.5375347,  8.359495, 180.1364},
                { 18.1148363, 56.792707,  49.1382},
                {-16.9603104,  8.448301, 152.5268},
                {-16.1765215,  1.978796,  98.2198},
                { 15.5567653,  8.863925,  97.4808},
                { 15.4846529,  0.186365, 221.5376},
                { 15.2150632,  8.996212, 168.2438},
                { 14.5047426,  6.771027, 161.1199},
                {-14.3873316, 45.815258,  55.0196},
                { 13.1351419, 12.002811, 262.6495},
                { 12.8776311, 75.278220, 200.3284},
                { 11.9867234, 65.241008, 201.6651},
                { 11.9385578, 18.870667, 294.6547},
                { 11.7030822, 22.009553,  99.8233},
                { 11.6018181, 64.604291, 213.5577},
                {-11.2617293, 11.498094, 154.1631},
                {-10.4664199,  0.578834, 232.7153},
                { 10.4333970,  9.237738, 138.3034},
                {-10.2377466, 49.747842, 204.6609},
                { 10.1934446,  2.147012, 106.5938},
                {-10.1280191,  1.196895, 250.4676},
                { 10.0289441,  2.133898, 332.3345},
                {-10.0034259,  0.173168,  27.3039}
            };

            const double twopi = 2 * Math.PI;
            const double piz180 = twopi / 360.0;
            int ym1950 = yr - 1950;

            /*
            Obliquity from Table 1 (2):
            OBLIQ# = 23.320556 (degrees)             Equation 5.5 (15)
            OBLIQD = OBLIQ# + sum[A cos(ft+delta)]   Equation 1 (5)
            */
            double sumc = 0;
            for (int i = 0; i < 47; i++)
            {
                double arg = piz180 * (ym1950 * t1[i,1] / 3600 + t1[i,2]);
                sumc += t1[i,0] * Math.Cos(arg);
            }
            obliq = 23.320556 + sumc / 3600;
            //obliq *= piz180; // deg to rad conversion

            /*
            Eccentricity from Table 4 (1):
            ECCEN sin(pi) = sum[M sin(gt+beta)]           Equation 4 (1)
            ECCEN cos(pi) = sum[M cos(gt+beta)]           Equation 4 (1)
            ECCEN = ECCEN sqrt[sin(pi)^2 + cos(pi)^2]
            */
            double esinpi = 0;
            double ecospi = 0;
            for (int i = 0; i < 19; i++)
            {
                double arg = piz180 * (ym1950 * t4[i,1] / 3600 + t4[i,2]);
                esinpi += t4[i,0] * Math.Sin(arg);
                ecospi += t4[i,0] * Math.Cos(arg);
            }
            
            double ecc = Math.Sqrt(esinpi * esinpi + ecospi * ecospi);
            eccen = ecc;


            /*
            Perihelion from Equation 4,6,7 (9) and Table 4,5 (1,3):
            PSI# = 50.439273 (seconds of degree)         Equation 7.5 (16)
            ZETA =  3.392506 (degrees)                   Equation 7.5 (17)
            PSI = PSI# t + ZETA + sum[F sin(ft+delta)]   Equation 7 (9)
            PIE = atan[ECCEN sin(pi) / ECCEN cos(pi)]
            OMEGVP = PIE + PSI + 3.14159                 Equation 6 (4.5)
            */

            double pie = Math.Atan2(esinpi, ecospi);
            double fsinfd = 0;

            for (int i = 0; i < 78; i++)
            {
                double arg = piz180 * (ym1950 * t5[i,1] / 3600 + t5[i,2]);
                fsinfd += t5[i,0] * Math.Sin(arg);
            }
            double psi = piz180 * (3.392506 + (ym1950 * 50.439273 + fsinfd) / 3600);
            omega_bar = (pie + psi + 0.5 * twopi) % twopi;

            // convert to conventions
            omega_bar = omega_bar*180/Math.PI - 180;
            if(omega_bar < 0){
                omega_bar = 360+omega_bar;
            }
            // prec_for_orbit = 360-omega_bar*180/pi;
        }

    }

}