class Circle {
    public int x;
    public int y;
    public int radius;

    public Circle() {
        Random random = new();
        x = random.Next(1,11);
        y = random.Next(1,11);
        radius = random.Next(1,3);
    }
}