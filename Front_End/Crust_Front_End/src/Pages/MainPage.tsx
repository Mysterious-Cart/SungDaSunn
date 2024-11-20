import "./MainPage.css"
import GetUser from "../Components/GetUser"
import React, { useEffect, useState } from "react";
import { Fragment } from "react/jsx-runtime"



function MainPage(){
    const GetUserData = async () => {
        const userList = [];
        <GetUser callback={userList}/>
        setUser(userList)
    }

    const [user, setUser] = useState([])
    useEffect(() => {
        GetUserData()

    })

    
    return(
        <div>
            <div style={{width: "30%", maxWidth:"300px", height: "100dvh", backgroundColor: "#343434"}}>
                
            </div>
            <div>
                
            </div>
        </div>
    );
}

export default MainPage;