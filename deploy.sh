#!/bin/bash
# deploy.sh — Run this ON your EC2 instance after uploading the project

set -e
echo "=== Building TodoApp ==="
cd /home/ubuntu/TodoApp
dotnet publish -c Release -o /var/www/todoapp

echo "=== Restarting service ==="
sudo systemctl restart todoapp
sudo systemctl status todoapp

echo "=== Done! App running at http://$(curl -s http://169.254.169.254/latest/meta-data/public-ipv4) ==="
