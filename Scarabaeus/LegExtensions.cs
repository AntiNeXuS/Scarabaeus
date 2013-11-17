namespace Scarabaeus
{
    using System;
    using System.Windows.Media.Media3D;

    public static class LegExtensions
    {
        const double herZnaet = 57.3;

        private static double GetLenght(double first, double second)
        {
            return Math.Sqrt(Sqr(first) + Sqr(second));
        }

        private static double Sqr(double value)
        {
            return value*value;
        }

        public static double Lenght(this Point3D point)
        {
            return GetLenght(point.X, point.Y);
        }

        public static bool CanReach(this Leg leg, Point3D newEnd)
        {
            // Проверим, что длина ноги больше, чем расстояние до точки
            if (leg.Coxa.Lenght + leg.Femur.Lenght + leg.Tibia.Lenght < GetLenght(newEnd.Lenght(), newEnd.Z))
                return false;

            return true;
        }

        /// <summary>
        /// Расчет точки пересечения двух окружностей - с координатами (0,0) и локальными координатами в плоскости Z0(A)
        /// </summary>
        /// <param name="leg"></param>
        /// <param name="newEnd"></param>
        /// <returns></returns>
        public static Point3D GetCirclesX(this Leg leg, Point3D newEnd)
        {
            var A = -2*newEnd.Lenght();
            var B = -2*newEnd.Z;
            var C = Sqr(newEnd.Lenght()) + Sqr(newEnd.Z) + Sqr(leg.Femur.Lenght) - Sqr(leg.Tibia.Lenght);
            var X0 = -A*C/(Sqr(A) + Sqr(B));
            var Y0 = -B*C/(Sqr(A) + Sqr(B));

            if (Sqr(C) > Sqr(leg.Femur.Lenght)*(Sqr(A) + Sqr(B)))
            {
                // нет решения
                return new Point3D();
            }
            else if (Math.Abs(Sqr(C) - Sqr(leg.Femur.Lenght) * (Sqr(A) + Sqr(B))) < 0.01)
            {
                // одна точка
                return new Point3D(X0, Y0, 0);
            }

            var D = Sqr(leg.Femur.Lenght) - (Sqr(C)/(Sqr(A) + Sqr(B)));
            var mult = Math.Sqrt(D / (Sqr(A) + Sqr(B)));
            var ax = X0 + B*mult;
            var bx = X0 - B*mult;
            var ay = Y0 - A*mult;
            var by = Y0 + A*mult;

            // выбираем точку сверху
            var result = new Point3D();
            result.X = ay > by ? ax : bx;
            result.Y = ay > by ? ay : by;

            return result;
        }

        public static bool CalculateAngles(this Leg leg, Point3D newEnd)
        {
            if (!leg.CanReach(newEnd)) 
                return false;

            leg.Coxa.Angle = leg.GetCoxaAngle(newEnd);

            leg.Femur.Angle = leg.GetFemurAngle(newEnd);
            leg.Tibia.Angle = leg.GetTibiaAngle(newEnd);

            return true;
        }

        private static float GetCoxaAngle(this Leg leg, Point3D newEnd)
        {
            var g = Math.Atan2(newEnd.Y, newEnd.X);

            return (float)(g * herZnaet + 90);
        }

        private static float GetFemurAngle(this Leg leg, Point3D newEnd)
        {
            var result = leg.GetCirclesX(newEnd);
            var g = Math.Atan2(result.Y, result.X);
            
            return (float)(g * herZnaet);
        }

        private static float GetTibiaAngle(this Leg leg, Point3D newEnd)
        {
            var g = Math.Atan2(newEnd.Z, newEnd.Lenght());
            var result = g * herZnaet - leg.GetFemurAngle(newEnd);
            
            return (float)(result);
        }
    }
}