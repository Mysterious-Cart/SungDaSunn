import React, { useEffect } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../Graphql/Queries";

function GetUser(){

    const [Get_User,{error, loading, data}] = useLazyQuery(Load_User)

    if(error){console.log(error); return}
    if(data){
        
    }

    return (
        <div>
            <button onClick={() => Get_User()}/>
        </div>
    );
};

export default GetUser;