using System;

public static class Snippy{
    public static long LongRandom(long min, long max, Random rand) {
        byte[] buf = new byte[8];
        rand.NextBytes(buf);
        long longRand = BitConverter.ToInt64(buf, 0);

        return (Math.Abs(longRand % (max - min)) + min);
    }
    
}