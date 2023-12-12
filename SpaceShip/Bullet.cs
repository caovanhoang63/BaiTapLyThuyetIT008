using System.Drawing;

namespace SpaceShip
{
    internal class Bullet : FlyingObject
    {
        
        
        public Bullet(int x, int y) : base(x, y)
        {
            _width = 20;
            _height = 25;
            _image = new Bitmap("./../../Assets/Image/LaserBullet.png");
            _image = new Bitmap(_image, _width, _height);
            _rectangle = new Rectangle(_x,_y, _width, _height);
            _speed = 10;
        }
        

        public override void Explosion(Graphics g)
        {
            
            return;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(_image, _x, _y);
            //graphics.DrawRectangle(Pens.Brown, _rectangle);
        }
        
        public override void Move()
        {
            _y -= _speed;
            _rectangle.Y = _y;
        }
        
    }
}