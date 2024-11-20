import React, { useEffect, useState } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../Graphql/Queries";

function GetUser({ callback }){

    const [Get_User,{error, loading, data}] = useLazyQuery(Load_User)
    if(error){console.log(error); return}

    useEffect(() => 
    {
        Get_User();
        if(loading){
            console.log("Loading..")
        }
        if(data){
            callback(data.user);
        }
    }
    , [data])
    
    return <div></div>;
};

export default GetUser;