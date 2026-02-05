using System;


namespace _2022_Test
{
    public static class mywedge
    {
        public static double angle = 36.1;
        public static double height = 0.011;
        public static double offset = 0.0;
        public static double velocity = 2330.0;
    }
    public static class Metal
    {
        public static double velocity = 5890.0;
    }
    public static class Prob
    {
        public static double element_pitch = 0.001;
        public static double element_count = 16;
    }
    public class Point_t
    {
        public double x;
        public double y;
    }

    public class BeamPoints
    {
        public Point_t[] elements;
        public Point_t[] wedgePoints;
        public Point_t focalPoint;
        public double[] FocalLaw;
    }
    public static class BeamCalc
    {
        private static double distance(Point_t a, Point_t b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2.0));
        }

        private static double find_max(double[] arr)
        {
            double max = 0;
            foreach (double d in arr)
            {
                if (d > max)
                {
                    max = d;
                }
            }

            return max;
        }

        private static double find_refract_angle(Point_t p_start, Point_t p_end)
        {
            // double hyp = distance(p_start, p_end);
            double dy = p_end.y - p_start.y;
            double dx = p_end.x - p_start.x;

            //return DEG(atan(dx / dy));
            return Math.Atan(dx / dy) * 180.0 / Math.PI;
        }

        public static BeamPoints LastFoundBeamPoints = null;
        public static double[] SearchFocalLaw(NSTEK.Models.ProbeModel probe, NSTEK.Models.MaterialModel material, NSTEK.Models.WedgeModel wedge, double angle, double focal_distance)
        {
            double focal_angle = angle;
            double focus_distance = focal_distance;

            Prob.element_pitch = probe.Pitch;
            Prob.element_count = probe.UsedElementsPerBeam;

            mywedge.angle = wedge.Angle;
            mywedge.height = wedge.FirstElementHeight;
            mywedge.offset = wedge.FirstElementOffset;
            mywedge.velocity = wedge.Velocity;

            Metal.velocity = material.Velocity;

            double hyp_center = Prob.element_pitch * (Prob.element_count - 1) / 2;

            Point_t element_center = new Point_t
            {
                x = Math.Cos(mywedge.angle * Math.PI / 180.0) * hyp_center,
                y = Math.Sin(mywedge.angle * Math.PI / 180.0) * hyp_center
            };
            Point_t element_start = new Point_t
            {
                x = 0,
                y = 0
            };


            /// todo:
            /// Matn.sin(DEG(focal_angle???)))
            double snell_angle = Math.Asin(mywedge.velocity * Math.Sin(focal_angle * Math.PI / 180.0) / Metal.velocity) * 180.0 / Math.PI;
            double steer_angle = snell_angle - mywedge.angle;

            Point_t surface_center = new Point_t
            {
                y = -mywedge.height,
                x = Math.Tan(snell_angle * Math.PI / 180.0) * Math.Abs(element_center.y - mywedge.height) + element_center.x
            };
            Point_t focal_point = new Point_t
            {
                y = surface_center.y - focus_distance,
                x = Math.Tan(focal_angle * Math.PI / 180.0) * Math.Abs(focus_distance) + surface_center.x
            };

            Point_t[] elements = new Point_t[(int)Prob.element_count];
            Point_t start = new Point_t
            {
                x = 0,
                y = 0
            };

            double[] delays = new double[(int)Prob.element_count];
            for (int i = 0; i < Prob.element_count; i++)
            {
                elements[i] = new Point_t
                {
                    x = start.x,
                    y = start.y,
                };
                //elements[i].x = start.x;
                //elements[i].y = start.y;

                start.x += Prob.element_pitch * Math.Cos(mywedge.angle * Math.PI / 180.0);
                start.y += Prob.element_pitch * Math.Sin(mywedge.angle * Math.PI / 180.0);
            }

            // newton_raphson(focal_point, element_start, surface_center, steer_angle, focal_angle, 0.00001, 99999999990);
            Point_t testpoint = new Point_t
            {
                x = 0.00001,
                y = -mywedge.height
            };
            Point_t[] sp = new Point_t[(int)Prob.element_count];
            for (int i = 0; i < Prob.element_count; i++)
            {
                sp[i] = new Point_t();
                Point_t p = SearchFitPoint(elements[i], focal_point, testpoint, 0.0, 0.0, Metal.velocity, mywedge.velocity);
                sp[i].x = p.x;
                sp[i].y = p.y;

                //newton_raphson(focal_point, elements[i], surface_center, steer_angle, focal_angle, 0.00001, 99999999990, testpoint);
                //printf("element: [%f, %f]\r\n", elements[i].x, elements[i].y);
                //printf("found center [%f, %f]\r\n", testpoint->x, testpoint->y);

                //delays[i] = (distance(*testpoint, elements[i]) / mywedge.velocity) + (distance(*testpoint, focal_point) / metal_velocity);
            }


            LastFoundBeamPoints = new BeamPoints();
            LastFoundBeamPoints.elements = elements;
            LastFoundBeamPoints.wedgePoints = sp;
            LastFoundBeamPoints.focalPoint = focal_point;
            LastFoundBeamPoints.FocalLaw = make_delays(elements, sp, focal_point);

            //double max = find_max(delays, (int)element_count);
            //for (int i = 0; i < element_count; i++)
            //{

            //    printf("delay[i]: %f, ", 1000000000 * (max - delays[i]));
            //}

            return LastFoundBeamPoints.FocalLaw;
        }

        public static Point_t SearchFitPoint(Point_t p1, Point_t p2, Point_t intersection, double angle_1, double angle_p2, double v1, double v2)
        {
            //
            double theta1 = find_refract_angle(p1, intersection);
            double theta2 = find_refract_angle(intersection, p2);

            double val1 = Math.Sin(theta1 * Math.PI / 180.0) * v2;
            double val2 = Math.Sin(theta2 * Math.PI / 180.0) * v1;

            double initial_error = calculate_snell_error(p1, p2, intersection, v1, v2);

            Point_t new_intersection = new Point_t();
            new_intersection.x = intersection.x;
            new_intersection.y = intersection.y;

            do
            {
                new_intersection.x += 0.0001;
                double cur_error = calculate_snell_error(p1, p2, new_intersection, v1, v2);
                // cur error must go lower
                if (cur_error - initial_error > 0)
                {
                    break;
                }
                initial_error = cur_error;

            } while (true);

            return new_intersection;
        }

        public static double calculate_snell_error(Point_t p1, Point_t p2, Point_t intersection, double v2, double v1)
        {
            double theta1 = find_refract_angle(p1, intersection);
            double theta2 = find_refract_angle(p2, intersection);

            double val1 = Math.Sin(theta1 * Math.PI / 180.0) * v2;
            double val2 = Math.Sin(theta2 * Math.PI / 180.0) * v1;

            return Math.Abs(val1 - val2);
        }


        /// <summary>
        /// multiply by 10^9 to make it nanos
        /// </summary>
        /// <param name="wp"></param>
        /// <param name="sp"></param>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static double[] make_delays(Point_t[] wp, Point_t[] sp, Point_t fp)
        {
            double[] p = new double[wp.Length];


            for (int i = 0; i < p.Length; i++)
            {
                double a = distance(wp[i], sp[i]) / mywedge.velocity;
                double b = distance(sp[i], fp) / Metal.velocity;

                p[i] = a + b;
            }

            double max = find_max(p);
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = (max - p[i]) * 1000000000;
            }

            return p;
        }
    }
}
