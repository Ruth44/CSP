pipeline {
  environment {
    PROJECT = "trainee-2020-6-bereketeab"
    APP_NAME = "csp-api"
    NAMESPACE = "csp"
    IMAGE_TAG = "gcr.io/${PROJECT}/${APP_NAME}:${env.BRANCH_NAME}.${BUILD_NUMBER}"
  }
    agent {
        kubernetes {
            defaultContainer 'jnlp'
            yaml '''
apiVersion: v1
kind: Pod
spec:
  serviceAccountName: jenkins-cv-comp
  containers:
  - name: gcloud
    image: gcr.io/cloud-builders/gcloud
    command:
    - cat
    tty: true
  - name: kubectl
    image: gcr.io/cloud-builders/kubectl
    command:
    - cat
    tty: true
'''
        }
    }
    stages {
        stage('Build & Push API') {
          steps {
            container('gcloud'){
              sh ('gcloud builds submit -t ${IMAGE_TAG} .')
            }
          }
        }
      
        stage('Deploy API') {    
          steps {
            container('kubectl'){
              sh("sed -i.bak 's#gcr.io/gcr-project/sample:1.0.0#${IMAGE_TAG}#' ./deployment/*.yaml")
              sh("kubectl --namespace=${NAMESPACE} apply -f ./deployment/")
            }
          }
        }
    }
}