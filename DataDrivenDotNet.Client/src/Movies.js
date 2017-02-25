import React, { Component } from 'react';

export default class Movies extends Component {
	constructor(props) {
		super(props);
		this.state = {
			movies: [],
			input: ''
		};
	}

	componentDidMount() {
		this.fetchMovies();

		//const username = 'test';

		const websocket = new WebSocket(`ws://localhost:52615/api/MovieSocket?username="Daniel"`);

		websocket.onopen = () => {
			console.warn('Websocket opened!');
			websocket.send("Hey I'm connected!");
		};

		websocket.onerror = () => {
			console.warn('error...');
		};

		websocket.onmessage = message => {
			switch(message.data) {
				case 'UPDATE_MOVIES':
					this.fetchMovies();
					break;

				default:
					break;
			}
		};
	}

	onInput = input => {
		this.setState({
			input
		});
	};

	onAdd = async () => {
		const body = JSON.stringify({
			Name: this.state.input
		});

		await fetch('http://localhost:52615/api/movie', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body
		});
		this.fetchMovies();
		this.setState({
			input: ''
		});
	};

	onDelete = async id => {
		await fetch(`http://localhost:52615/api/movie/${id}`, { method: 'DELETE' });
		this.fetchMovies();
	};

	fetchMovies = async () => {
		const response = await fetch('http://localhost:52615/api/movie');
		const movies = await response.json();

		this.setState({
			movies
		});
	};

	render() {
		return (
			<div>
				<ul>
					{
						this.state.movies.map(x => (
							<li key={x.Id}>
								<div>
									{x.Name}
									<button onClick={() => this.onDelete(x.Id)}>Delete</button>
								</div>
							</li>
						))
					}
				</ul>
				<input type="text" value={this.state.input} onChange={e => this.onInput(e.target.value)} />
				<button onClick={this.onAdd}>Add Movie</button>
			</div>
		);
	}
}
