import { gql } from "@apollo/client";

export const LoginRequest = gql`
    mutation login($name: String!, $password: String!){
        login(input: {username: $name, password: $password} ){
            loginToken{
                sessionId
                user{
                    name
                }
            }
        }
    }
`