import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class SignInForm extends Component {
    constructor() {
        super();

        this.state = {
            username: '',
            password: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(e) {
        let target = e.target;
        let value = target.type === 'checkbox' ? target.checked : target.value;
        let name = target.name;

        this.setState({
          [name]: value
        });
    }

    handleSubmit(e) {
        e.preventDefault();

        fetch('http://localhost:5000/user/authenticate', {
          method: 'POST',
          mode: 'cors', // defaults to same-origin
          headers: {
            'Content-Type': 'application/json',
            'content-length': '50',
            'Accept': '*/*'
          },
          body: JSON.stringify({
            Username: this.state.username,
            Password: this.state.password
          })}).then(res => {
            if (res.ok) {
              alert('Succesfully authenticated..')
              this.props.history.push("/search");
            }else{
              alert('Failed to authenticate!')
            }
          }).catch(error => alert('Failed to signed up!'));

        console.log('The form was submitted with the following data:');
        console.log(this.state);
    }

    render() {
        return (
        <div className="FormCenter">
            <form onSubmit={this.handleSubmit} className="FormFields">
              <div className="FormField">
                <label className="FormField__Label" htmlFor="name">Username</label>
                <input type="text" id="username" className="FormField__Input" placeholder="Enter your username" name="username" value={this.state.name} onChange={this.handleChange} />
              </div>
              <div className="FormField">
                <label className="FormField__Label" htmlFor="password">Password</label>
                <input type="password" id="password" className="FormField__Input" placeholder="Enter your password" name="password" value={this.state.password} onChange={this.handleChange} />
              </div>
              <div className="FormField">
                  <button className="FormField__Button mr-20">Sign In</button> <Link to="/" className="FormField__Link">Create an account</Link>
              </div>
            </form>
          </div>
        );
    }
}

export default SignInForm;
