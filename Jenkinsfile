pipeline {
    agent any

    stages {
        stage('Preparing') {
            steps {
                dir("src") {}
            }
        }

        stage('Cleaning project') {
            steps {
                dotnetClean(
                    configuration: 'Release',
                    nologo: true
                )
            }
        }

        stage('Building project') {
            steps {
                dotnetBuild(
                    configuration: 'Release',
                    nologo: true
                )
            }
        }

        stage('Packing') {
            steps {
                dotnetPack(
                    configuration: 'Release',
                    nologo: true
                )
            }
        }

        stage('Archive artifacts') {
            steps {
                archiveArtifacts(artifacts: "NullCode.WASM.LocalStorage\\bin\\Release\\*.nupkg")
            }
        }

        stage('Pushing to NuGet') {
            when {
                expression {
                    params.should_push == true
                }
            }
            steps {
                dotnetNuGetPush(
                    apiKeyId: params.nuget_api_key,
                    noSymbols: true,
                    skipDuplicate: false
                )
            }
        }

        stage('Clean workspace') {
            steps {
                cleanWs (
                    cleanWhenNotBuilt: true,
                    deleteDirs: true,
                    disableDeferredWipeout: true,
                    notFailBuild: true
                )
            }
        }
    }
}