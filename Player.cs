using System;
using SplashKitSDK;

namespace Idimon
{
    public class Player : Character
    {
        private const double Speed = 200; // Pixels per second
        public Vector2D MoveVector { get; private set; }
        private Queue<Point2D> _moveQueue;
        private double _moveTimer;
        private Window _window;
        private const double MoveDuration = 0.2;
        string path = "img\\";

        public Player(string name, List<string> imagePaths, Point2D position, Window window) : base(name, imagePaths, position, window)
        {
            LoadImages();
            // _moveQueue = new Queue<Point2D>();
            // _moveTimer = 0;
            _window = window;
            MoveVector = new Vector2D() { X = 0, Y = 0 };
        }

        public override void LoadImages()
        {
            _images["up"] = new Bitmap[4];
            _images["down"] = new Bitmap[4];
            _images["left"] = new Bitmap[4];
            _images["right"] = new Bitmap[4];

            for (int i = 0; i < 4; i++)
            {
                _images["up"][i] = SplashKit.LoadBitmap($"up_{i}", $"{path}up{i}.png");
                _images["down"][i] = SplashKit.LoadBitmap($"down_{i}", $"{path}down{i}.png");
                _images["left"][i] = SplashKit.LoadBitmap($"left_{i}", $"{path}left{i}.png");
                _images["right"][i] = SplashKit.LoadBitmap($"right_{i}", $"{path}right{i}.png");
            }
        }

        public void QueueMove(double x, double y)
        {
            _moveQueue.Enqueue(new Point2D() { X = x, Y = y });
        }

        // public void Update(double deltaTime)
        // {
        //     UpdateAnimation(deltaTime);
        //     UpdateMovement(deltaTime);
        // }

        public void Update(double deltaTime)
        {
            if (SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.LeftKey) || SplashKit.KeyDown(KeyCode.RightKey))
            {
                UpdateAnimation(deltaTime);
            }
        }

        private void UpdateMovement(double deltaTime)
        {
            if (_moveQueue.Count > 0)
            {
                _moveTimer += deltaTime;
                if (_moveTimer >= MoveDuration)
                {
                    _moveTimer -= MoveDuration;
                    Point2D nextMove = _moveQueue.Dequeue();
                    Move(nextMove.X, nextMove.Y);
                }
            }
        }

        public override void Draw()
        {
            // _window.DrawBitmap(AnimationFrames[CurrentFrame], _position.X, _position.Y);
            _window.DrawBitmap(_images[_currentDirection][_currentFrame], _position.X, _position.Y);
        }

        public void HandleInput(double deltaTime)
        {
            double dx = 0, dy = 0;
            bool isMoving = false;

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                UpdateDirection("up");
                dy = -Speed * deltaTime;
                isMoving = true;
            }
            else if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                UpdateDirection("down");
                dy = Speed * deltaTime;
                isMoving = true;
            }
            else if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                UpdateDirection("left");
                dx = -Speed * deltaTime;
                isMoving = true;
            }
            else if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                UpdateDirection("right");
                dx = Speed * deltaTime;
                isMoving = true;
            }

            MoveVector = new Vector2D() { X = dx, Y = dy };

            if (isMoving)
            {
                Move(dx, dy);
                UpdateAnimation(deltaTime); // Update the animation frame only when moving
            }
            else
            {
                // Reset the animation timer when not moving
                _animationTimer = 0;
                _currentFrame = 0;
            }
        }

        public Point2D Position => _position;
    }
}
