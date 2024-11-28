import React, { useEffect, useState } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../../Graphql/Queries";

function GetUser({result}){
    
    const [refetch,{error, loading, data}] = useLazyQuery(Load_User)
    if(error){console.log(error); return<></>}

    useEffect(() => 
    {
        const fetchdata = async () => {
            await refetch();
        } 

        fetchdata();    

        if(data && data.length !== 0){
            result(data);
        }
        
    }
    , [data])
    
    return <></>;
};

export default GetUser;