import { gql } from "@apollo/client";

export const Load_User = gql`
    query GetUser{
        user{
            name
            groups{
                groupInfo{
                    name
                    id
                }
            }
        }
    }

`

export const Get_User_Friend = gql`
    query{
        user{
            id
            name
            groups{
            groupId
            }
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

`
