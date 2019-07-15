import React, { Component } from 'react';

class MovieSearchForm extends Component {
    constructor() {
        super();

        this.state = {
            ImdbId: '',
            Title: '',
            MovieInfo: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(e) {
        let target = e.target;
        let value = target.value;
        let name = target.name;

        this.setState({
          [name]: value
        });
    }

    handleSubmit(e) {
        e.preventDefault();

        fetch(`http://localhost:5000/moviesearch/title?=${this.state.Title}`)
        .then(res => {
            if (res.ok) { 
                return res.text()
            }else{
                fetch(`http://localhost:5000/moviesearch/imdb?=${this.state.ImdbId}`)
                .then(res => {
                    if (res.ok) {
                        return res.text()
                    }else{
                        alert('Movie you are looking for could not be found!');
                        return null;
                    }
                  })
                  .then(textData => {
                    this.setState({
                        MovieInfo: textData
                      });
                  }).catch(error => {
                        alert('Movie you are looking for could not be found!');
                        throw(error);
                  });

                  return null
            }
        })
        .then(textData => {
            this.setState({
                MovieInfo: textData
              });          
            }).catch(error => alert('Movie you are looking for could not be found!'));
    }

    render() {
        return (
        <div className="FormCenter">
            <form onSubmit={this.handleSubmit} className="FormFields">
            <div className="FormField">
                <label className="FormField__Label" htmlFor="movieInfo">Movie Info</label>
                <text id="movieInfo" name="MovieInfo" edgeMode>{this.state.MovieInfo}</text>
            </div>
            <div className="FormField">
                <label className="FormField__Label" htmlFor="imdbId">ImdbId</label>
                <input type="text" id="imdbId" className="FormField__Input" placeholder="Enter a movie ImdbId" name="ImdbId" value={this.state.ImdbId} onChange={this.handleChange} />
            </div>
            <div className="FormField">
                <label className="FormField__Label" htmlFor="title">Title</label>
                <input type="text" id="title" className="FormField__Input" placeholder="Enter a movie title" name="Title" value={this.state.Title} onChange={this.handleChange} />
            </div>
            <div className="FormField">
                <button className="FormField__Button mr-20">Search</button>
            </div>
            </form>
        </div>
        );
    }
}

export default MovieSearchForm;
