using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidSquare {
    class Program {
        static void Main(string[] args) {
            int[] input1 = new int[] { 1, 0 };
            int[] input2 = new int[] { -1, 0 };
            int[] input3 = new int[] { 0, 1 };
            int[] input4 = new int[] { 0, -1 };
            bool ouput = ValidSquare(input1, input2, input3, input4);
            if (ouput) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("no");
            }
            Console.ReadLine();
        }

        static public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4) {
            int maxX = -10001;
            int minX = 10001;
            int maxY = -10001;
            int minY = 10001;

            maxX = testMax(p1, maxX,0);
            maxX = testMax(p2, maxX,0);
            maxX = testMax(p3, maxX,0);
            maxX = testMax(p4, maxX,0);

            maxY = testMax(p1, maxY, 1);
            maxY = testMax(p2, maxY, 1);
            maxY = testMax(p3, maxY, 1);
            maxY = testMax(p4, maxY, 1);

            minX = testMin(p1, minX, 0);
            minX = testMin(p2, minX, 0);
            minX = testMin(p3, minX, 0);
            minX = testMin(p4, minX, 0);

            minY = testMin(p1, minY, 0);
            minY = testMin(p2, minY, 0);
            minY = testMin(p3, minY, 0);
            minY = testMin(p4, minY, 0);

            Console.WriteLine(maxX);
            Console.WriteLine(maxY);
            Console.WriteLine(minX);
            Console.WriteLine(minY);

            if (IsBottomLeft(p1, minX, minY)){
                if(IsTopRight(p2, maxX, maxY)) {
                    if(IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    }else if(IsTopLeft(p3, minX, maxY)) {
                        if(IsBottomRight(p4, maxX, minY)){
                            return true;
                        }
                    }
                }else if(IsTopRight(p3, maxX, maxY)) {
                    if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p4, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p4, maxX, maxY)) {
                    if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p3, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    }
                }
            }else if(IsBottomLeft(p2, minX, minY)){
                if (IsTopRight(p1, maxX, maxY)) {
                    if (IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p3, minX, maxY)) {
                        if (IsBottomRight(p4, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p3, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p4, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p4, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p3, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                }
            }else if(IsBottomLeft(p3, minX, minY)) {
                if (IsTopRight(p1, maxX, maxY)) {
                    if (IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p4, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p2, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p4, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p4, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p4, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                }
            } else if (IsBottomLeft(p4, minX, minY)) {
                if (IsTopRight(p1, maxX, maxY)) {
                    if (IsTopLeft(p3, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p2, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p3, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p3, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                } else if (IsTopRight(p3, maxX, maxY)) {
                    if (IsTopLeft(p1, minX, maxY)) {
                        if (IsBottomRight(p2, maxX, minY)) {
                            return true;
                        }
                    } else if (IsTopLeft(p2, minX, maxY)) {
                        if (IsBottomRight(p1, maxX, minY)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static public bool IsBottomLeft(int[] p1, int minX, int minY) {
            if(p1[0] == minX && minY == p1[1]) {
                return true;
            }
            return false;
        }

        static public bool IsTopRight(int[] p1, int maxX, int maxY) {
            if (p1[0] == maxX && maxY == p1[1]) {
                return true;
            }
            return false;
        }

        static public bool IsBottomRight(int[] p1, int maxX, int minY) {
            if (p1[0] == maxX && minY == p1[1]) {
                return true;
            }
            return false;
        }

        static public bool IsTopLeft(int[] p1, int minX, int maxY) {
            if (p1[0] == minX && maxY == p1[1]) {
                return true;
            }
            return false;
        }

        static public int testMax(int[] p1, int max, int mode) {
            if (mode == 1) {
                if (p1[1] > max) {
                    max = p1[1];
                }
            } else {
                if (p1[0] > max) {
                    max = p1[0];
                }
            }
            return max;
        }

        static public int testMin(int[] p1, int min, int mode) {
            if (mode == 1) {
                if (p1[1] < min) {
                    min = p1[1];
                }
            } else {
                if (p1[0] < min) {
                    min = p1[0];
                }
            }
            return min;
        }
    }
}
