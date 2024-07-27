﻿using System;
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
        private Map _map; // Reference to the Map instance
        string path = "img\\PlayerIMG\\";

        public Player(string name, List<string> imagePaths, Point2D position, Window window, Map map) : base(name, imagePaths, position, window)
        {
            LoadImages();
            _window = window;
            MoveVector = new Vector2D() { X = 0, Y = 0 };
            _moveQueue = new Queue<Point2D>();
            _moveTimer = 0;
            _map = map; // Initialize the Map reference
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

        public void Update(double deltaTime)
        {
            UpdateMovement(deltaTime);

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
                    if (_map.CanMoveTo((int)(_position.X + nextMove.X), (int)(_position.Y + nextMove.Y)))
                    {
                        Move(nextMove.X, nextMove.Y);
                    }
                }
            }

            double dx = 0, dy = 0;
            bool isMoving = false;
            Point2D newPosition = _position;
            int newX = 0, newY = 0;

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

            newPosition.X += dx;
            newPosition.Y += dy;
            int newdx, newdy;
            if(dx > 0)
            {
                newdx = 32;
                newdy = 0;
            }
            else if(dx < 0)
            {
                newdx = -32;
                newdy = 0;
            }
            else if(dy > 0)
            {
                newdx = 0;
                newdy = 32;
            }
            else
            {
                newdx = 0;
                newdy = -32;
            }
            newX = (int)(newPosition.X + 32 + newdx) / 64 + 1;
            newY = (int)(newPosition.Y + 32 + newdy) / 64 + 1;
            // Console.WriteLine(newX + " " + newY);

            MoveVector = new Vector2D() { X = dx, Y = dy };

            if (isMoving && _map.CanMoveTo((int)newY, (int)newX))
            {
                Move(dx, dy);
                UpdateAnimation(deltaTime); // Update the animation frame only when moving
            }
            else if (!isMoving)
            {
                // Reset the animation timer when not moving
                _animationTimer = 0;
                _currentFrame = 0;
            }
        }

        public override void Draw()
        {
            _window.DrawBitmap(_images[_currentDirection][_currentFrame], 64 * (int)(15 / 2), 64 * (12 / 2));
            _window.DrawBitmap(_images[_currentDirection][_currentFrame], 0, 0);
        }

        public Point2D Position => _position;
        public Map Map => _map;
    }
}
