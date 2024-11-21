import React, { useEffect, useState } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../../Graphql/Queries";

function GetUser({result}){
    
    const [LoadUser,{error, loading, data}] = useLazyQuery(Load_User)
    if(error){console.log(error); return}

    useEffect(() => 
    {
        LoadUser();
        
        if(data){
            result(data);
        }
        
    }
    , [LoadUser,data])
    
    return <div></div>;
};

export default GetUser;