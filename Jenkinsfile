
pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                echo 'Code is available in Jenkins workspace'
            }
        }

        stage('Build Info') {
            steps {
                echo 'Repository checkout pipeline is running'
                sh 'git --version'
            }
        }
    }
}
