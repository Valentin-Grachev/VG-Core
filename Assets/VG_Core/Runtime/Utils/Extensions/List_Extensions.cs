using System.Collections.Generic;

namespace VG
{
    public static class List_Extensions
    {
        
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, list.Count);

                T temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }
        }

        public static T GetRandom<T>(this List<T> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randomIndex];
        }



    }
}



