import React, { useEffect, useState } from "react";
import { useLazyQuery, gql } from "@apollo/client";
import { Load_User } from "../Graphql/Queries";

function GetUser(){

    const [Get_User,{error, loading, data}] = useLazyQuery(Load_User)
    const [user, setUser] = useState([])
    if(error){console.log(error); return}
    useEffect(() => 
    {
        console.log(data);
    }
    , [data])

    return (
       <div>
            <button onClick={Get_User}/>
            <h1> David</h1>
       </div>
    );
};

export default GetUser;