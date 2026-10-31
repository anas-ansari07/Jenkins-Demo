
pipeline {
    agent any

    environment {
        DOTNET_IMAGE = 'mcr.microsoft.com/dotnet/sdk:10.0'
        PROJECT_FILE = 'WebApiBasicsTut.csproj'
        JENKINS_VOLUME = 'jenkins_home_fresh'
        NUGET_VOLUME = 'dotnet_nuget_cache'
    }

    stages {

        stage('Checkout') {
            steps {
                echo 'Checking out source code from GitHub'
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                echo 'Restoring NuGet packages'

                sh '''
                    docker run --rm \
                      -v ${JENKINS_VOLUME}:/var/jenkins_home \
                      -v ${NUGET_VOLUME}:/root/.nuget/packages \
                      -w "$WORKSPACE" \
                      ${DOTNET_IMAGE} \
                      dotnet restore ${PROJECT_FILE}
                '''
            }
        }

        stage('Build') {
            steps {
                echo 'Building .NET 10 API'

                sh '''
                    docker run --rm \
                      -v ${JENKINS_VOLUME}:/var/jenkins_home \
                      -v ${NUGET_VOLUME}:/root/.nuget/packages \
                      -w "$WORKSPACE" \
                      ${DOTNET_IMAGE} \
                      dotnet build ${PROJECT_FILE} \
                      --configuration Release \
                      --no-restore
                '''
            }
        }

        stage('Test') {
            steps {
                echo 'Running automated tests'

                sh '''
                    docker run --rm \
                      -v ${JENKINS_VOLUME}:/var/jenkins_home \
                      -v ${NUGET_VOLUME}:/root/.nuget/packages \
                      -w "$WORKSPACE" \
                      ${DOTNET_IMAGE} \
                      dotnet test ${PROJECT_FILE} \
                      --configuration Release
                '''
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing .NET API'

                sh '''
                    docker run --rm \
                      -v ${JENKINS_VOLUME}:/var/jenkins_home \
                      -v ${NUGET_VOLUME}:/root/.nuget/packages \
                      -w "$WORKSPACE" \
                      ${DOTNET_IMAGE} \
                      dotnet publish ${PROJECT_FILE} \
                      --configuration Release \
                      --no-restore \
                      --output "$WORKSPACE/publish"
                '''
            }
        }
    }

    post {
        success {
            echo 'SUCCESS: .NET API pipeline completed!'

            archiveArtifacts artifacts: 'publish/**',
                             allowEmptyArchive: false
        }

        failure {
            echo 'FAILURE: .NET API pipeline failed!'
        }

        always {
            echo 'Pipeline execution finished.'
        }
    }
}
