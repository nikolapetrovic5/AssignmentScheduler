import * as React from 'react'
import TextField from '@mui/material/TextField/TextField'

export default class Login extends React.Component<{}> {
    render() {
        return (
            <div style={{ display: "block", alignItems: "center", width: "100%", height: "100%" }}>
                <TextField
                    id="emailTextField"
                    label="Email"
                    variant="outlined"/>
                <TextField
                    id="passwordTextField"
                    label="Password"
                    variant="outlined"/>
            </div>
        );
    };
}


