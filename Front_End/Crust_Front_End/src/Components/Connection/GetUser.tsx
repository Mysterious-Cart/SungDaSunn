import React, { useEffect, useState } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../../Graphql/Queries";

function useGetUser(){
    
    const [refetch,{error, loading, data}] = useLazyQuery(Load_User)
    const [userlist, setUserList] = useState([])
    if(error){console.log(error); return<></>}

    useEffect(() => 
    {
        const fetchdata = async () => {
            await refetch();
        } 

        if(data && data.length !== 0){
            setUserList(data);
        }else{
            if(loading === false){
                fetchdata();
            }
            
        }
        
    }
    , [data, loading])
    
    return userlist;
};

export default useGetUser;