namespace Scarabaeus
{
    public class LegPart
    {
        /*private float _angle;

        private float _minAngle = 0;
        private float _maxAngle = 180;
        private float _defaultAngle = 90;*/

        /// <summary>
        /// Длина конечности
        /// </summary>
        public float Lenght;

        /// <summary>
        /// Угол поворота сервы относительно исходного нуля (положения в 90)
        /// </summary>
        public float Angle;
        /*{
            get
            {
                return _angle;
            }

            set
            {
                if (value < _minAngle)
                {
                    _angle = _minAngle;
                    return;
                }

                if (value > _maxAngle)
                {
                    _angle = _maxAngle;
                    return;
                }

                _angle = value;
            }
        }*/

        public LegPart(float lenght, float angle = 90)
        {
            Lenght = lenght;
            Angle = angle;
            /*_angle = angle;*/
        }
    }
}