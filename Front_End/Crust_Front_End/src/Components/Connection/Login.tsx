import { useEffect, useState } from "react";
import { useMutation } from "@apollo/client";
import {LoginRequest} from "../../Graphql/Mutation"

function useLoginAsDev(){
    const [info, setInfo] = useState([]);
    const [refetch,{error, data}] = useMutation(LoginRequest, {variables:{name: "Dev", password:"Admin"}})
    if(error){console.log(error); return<></>}
    
    useEffect(() => 
    {
        const fetchdata = async () => {
            await refetch();
        } 

        fetchdata();

        if(data && data.length !== 0){
            setInfo(data)
        }
    }
    , [])

    return data;
    
}

export default useLoginAsDev;