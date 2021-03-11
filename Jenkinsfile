pipeline {
    agent any

    stages {
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