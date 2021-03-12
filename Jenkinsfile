pipeline {
    agent any

    stages {
        stage('Cleaning project') {
            steps {
                dotnetClean(
                    configuration: 'Release',
                    nologo: true,
                    workDirectory: "src/"
                )
            }
        }

        stage('Building project') {
            steps {
                dotnetBuild(
                    configuration: 'Release',
                    nologo: true,
                    workDirectory: "src/"
                )
            }
        }

        stage('Packing') {
            steps {
                dotnetPack(
                    configuration: 'Release',
                    nologo: true,
                    workDirectory: "src/"
                )
            }
        }

        stage('Archive artifacts') {
            steps {
                archiveArtifacts(artifacts: "src\\NullCode.WASM.LocalStorage\\bin\\Release\\*.nupkg")
            }
        }

        stage('Pushing to NuGet') {
            when {
                expression {
                    params.should_push == true
                }
            }
            steps {
                step {
                    script {
                        def files = findFiles(glob: "src\\NullCode.WASM.LocalStorage\\bin\\Release\\*.nupkg")

                        if(files.length == 0) {
                            throw new Exception("Can't find .nupkg file!")
                        }

                        dotnetNuGetPush(
                            root: files[0].getPath(),
                            apiKeyId: "nuget_api_key",
                            skipDuplicate: false,
                            workDirectory: "src/"
                        )
                    }
                }
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