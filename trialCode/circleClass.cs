class Circle {
    private int x;
    private int y;
    private int radius;
    public Circle() {
        Random random = new();
        X = random.Next(1,11);
        Y = random.Next(1,11);
        Radius = random.Next(1,3);
    }

    public int X {
        get { return x; }

        set {
            x = value;
        }
    }

    public int Y {
        get {return y;}

        set {
            y = value;
        }
    }

    public int Radius {
        get {return radius;}

        set {
            radius = value;
        }
    }
}