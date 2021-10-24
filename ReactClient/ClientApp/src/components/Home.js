import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div className="row my-3">
            <div className="card">
                <div class="card-body">
                    <h2 className="card-title">Users</h2>
                    <Link to="/user">
                        <button type="button" className="btn btn-primary">
                            See users
                        </button>
                    </Link>
                </div>
            </div>
        </div>
    );
  }
}
