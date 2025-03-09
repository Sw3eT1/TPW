class Circle {
    private int x;
    private int y;
    private int radius;

    private Color color;

    public Circle() {
        Random random = new();
        Radius = random.Next(10,30);
        Color = Color.Red;
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

    public Color Color {
        get {return color;}
        set {
            color = value;
        }
    }
}