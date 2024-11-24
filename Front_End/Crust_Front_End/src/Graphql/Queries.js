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