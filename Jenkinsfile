pipeline {
    agent any

    stages {

        stage('Clone Code') {
            steps {
                git branch: 'main',
                url: 'https://github.com/mr-xivam/TodoAppp.git'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t todoapp:latest .'
            }
        }

        stage('Stop Old Container') {
            steps {
                sh 'docker rm -f todoapp || true'
            }
        }

        stage('Run Container') {
            steps {
                sh '''
                docker run -d \
                --name todoapp \
                -p 8081:5000 \
                todoapp:latest
                '''
            }
        }
    }
}
