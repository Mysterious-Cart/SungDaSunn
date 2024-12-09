import { gql } from "@apollo/client";

export const LoginRequest = gql`
    mutation login($name: String!, $password: String!){
        login(input: {username: $name, password: $password} ){
            loginToken{
                sessionId
                user{
                    name
                    friends_List{
                        friend{
                            id
                            name
                        }
                    }
                    friended_List{
                        user{
                            id
                            name
                        }
                    }
                }
                
            }
        }
    }
`

export const Send_Message = gql`
    mutation send_Message($groupId: Int!, $senderId: Int!, $sessionId: Int!, $messages: String!){
        send_Message(input: {
            groupId: $groupId
            senderId: $senderId
            sessionId: $sessionId
            messages: $messages
        }){
            message_Result{
                sent
                sent_Time
            }
        }
    }
`