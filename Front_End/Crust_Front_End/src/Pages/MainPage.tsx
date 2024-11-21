import "./MainPage.css"
import UserProfileList from "../Components/UserProfileList"
import React, { useEffect, useState } from "react";
import { Fragment } from "react/jsx-runtime"
import { Card } from "@mui/material";

function MainPage(){
    
    return(
        <div style={{width:"100%"}}>
            <Card style={{width: "100%", maxWidth:"300px", height: "100dvh"}}>
                <UserProfileList width="100%" height="100%"/>
            </Card>
            <div>

            </div>
        </div>
            
    );
}

export default MainPage;