To play: launch two browsers(one in incognito mode) and login into 2 different accounts

Setup:
Frontend:
yarn install;
in source create file: aws-exports.js;
inside the file paste:

```
const awsmobile = {
    "aws_project_region": "eu-central-1",
    "aws_cognito_identity_pool_id": "eu-central-1:53caba21-4f8b-4f4c-becf-af1aedc3c405",
    "aws_cognito_region": "eu-central-1",
    "aws_user_pools_id": "eu-central-1_jvYBzSwNe",
    "aws_user_pools_web_client_id": "1s15mbpqua63v46o0qbgd20m71",
    "oauth": {
        "domain": "chickencoop-dev.auth.eu-central-1.amazoncognito.com",
        "scope": [
            "email",
            "openid",
            "profile",
            "aws.cognito.signin.user.admin"
        ],
        "redirectSignIn": "https://master.d38ezml3btx9eh.amplifyapp.com/signin/",
        "redirectSignOut": "https://master.d38ezml3btx9eh.amplifyapp.com/signout/",
        "responseType": "code"
    },
    "federationTarget": "COGNITO_USER_POOLS",
    "aws_user_files_s3_bucket": "chickencoopimagesbucket164829-dev",
    "aws_user_files_s3_bucket_region": "eu-central-1"
};


export default awsmobile;
```
yarn serve;

Backend:
update-database;
download aws toolkit for visual studio;
open the aws toolkit;
add a new profile;
profile name: default;
access key paste: AKIASGCPWM7NGNSACK5L;
secret access key paste: 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;
account number: 150490998746;
Launch the server;
