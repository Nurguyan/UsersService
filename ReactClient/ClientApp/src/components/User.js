import React, { Component } from 'react';
import { variables } from './Variables.js'

export class User extends Component {

    constructor(props) {
        super(props);

        this.state = {
            users: [],
            modalTitle: "",
            Id: 0,
            Surname: "",
            Age: 0,
            Sex: 0,
            IsActive: false,
            Phones: []
        }
    }

    changeSurname = (e) => {
        this.setState({ Surname: e.target.value });
    }

    changeAge = (e) => {
        this.setState({ Age: e.target.value });
    }

    changeSex = (e) => {
        this.setState({ Sex: e.target.value });
    }

    changeIsActive = (e) => {
        this.setState(prevState => ({
            IsActive: !prevState.IsActive
        }));
    }

    changePhone = (p, i) => {
        // 1. Make a shallow copy of the items
        let items = [...this.state.Phones];
        // 2. Make a shallow copy of the item you want to mutate
        let item = { ...items[i] };
        // 3. Replace the property you're intested in
        item.Number = p.Number;
        // 4. Put it back into our array. N.B. we *are* mutating the array here, but that's why we made a copy first
        items[i] = item;
        // 5. Set the state to our new copy
        this.setState({ Phones: items });
    };

    //обновить список пользователей GET /api/user
    refreshList() {
        fetch(variables.API_URL + 'user')
            .then(response => response.json())
            .then(data => {
                this.setState({ users: data });
            });
    }

    componentDidMount() {
        this.refreshList();
        this.timer = setInterval(() => this.refreshList(), 1000); //поллинг апи каждые 1000 мс
    }

    //получить пользователя GET /api/user/{id}
    getUserById(id) {
        fetch(variables.API_URL + 'user/' + id, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then((result) => {
                this.setState({
                    Id: result.Id,
                    Surname: result.Surname,
                    Age: result.Age,
                    Sex: result.Sex,
                    IsActive: result.IsActive,
                    Phones: result.Phones
                }); console.log(this.state.IsActive);
            }, (error) => {
                alert('Failed to get used by id');
            })
    }

    editClick(user) {
        this.getUserById(user.Id);
        this.setState({
            modalTitle: "Edit User"
        });
    }

    //обновить пользователя PUT /api/user/
    updateClick() {
        fetch(variables.API_URL + 'user', {
            method: 'PUT',
            mode: 'cors', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: this.state.Id,
                Surname: this.state.Surname,
                Age: this.state.Age,
                Sex: this.state.Sex,
                IsActive: this.state.IsActive,
                Phones: this.state.Phones
            })
        })
            .then(res => {
                if (res.ok) {
                    this.refreshList();
                }
            })
    }

    //удалить пользователя DELETE /api/user/{id}
    deleteClick(id) {
        if (window.confirm('Вы уверены, что хотите удалить пользователя?')) {
            fetch(variables.API_URL + 'user/' + id, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            }).then(res => {
                if (res.ok) {
                    this.refreshList();
                }
            })
        }
    }

    render() {
        const {
            users,
            modalTitle,
            Id,
            Surname,
            Age,
            Sex,
            IsActive,
            Phones
        } = this.state;

        return (
            <div className="container-fluid" >
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>№</th>
                            <th>Фамилия</th>
                            <th>Возраст</th>
                            <th>Пол</th>
                            <th>Активен</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(u =>
                            <tr key={u.Id}>
                                <td>{u.Id}</td>
                                <td>{u.Surname}</td>
                                <td>{u.Age}</td>
                                <td>{u.Sex}</td>
                                <td><input type="checkbox" checked={u.IsActive} onChange={this.changeIsActive} disabled />&nbsp;</td>
                                <td>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#exampleModal"
                                        onClick={() => this.editClick(u)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>

                                    <button type="button" className="btn btn-light mr-1" onClick={() => this.deleteClick(u.Id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />
                                            <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" />
                                        </svg>
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <div className="modal" id="exampleModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="input-group mb-3">
                                    <span className="input-group-text">Фамилия</span>
                                    <input type="text" className="form-control"
                                        value={Surname}
                                        onChange={this.changeSurname} />
                                </div>
                                <div className="input-group mb-3">
                                    <span className="input-group-text">Возраст</span>
                                    <input type="text" className="form-control" value={Age} onChange={this.changeAge} />
                                </div>
                                <div className="input-group mb-3">
                                    <span className="input-group-text">Пол</span>
                                    <select id="lang" onChange={this.changeSex} value={this.state.Sex}>
                                        <option value="NotKnown">Неизвестно</option>
                                        <option value="Male">Мужчина</option>
                                        <option value="Female">Женщина</option>
                                        <option value="NotApplicable">Неприменимо</option>
                                    </select>
                                </div>
                                <div className="form-check">
                                    <input className="form-check-input" type="checkbox" id="isActiveCheckbox" checked={IsActive} onChange={this.changeIsActive}></input>
                                    <label className="form-check-label">
                                        Активен
                                    </label>
                                </div>


                                {Phones.map((phone, index) =>
                                    <div className="input-group mb-3">
                                        <span className="input-group-text">Телефон</span>
                                        <input type="text" className="form-control" key={phone.Id} defaultValue={phone.Number} onChange={() => { this.changePhone(phone, index) }} />
                                    </div>
                                )}

                                {Id !== 0 ?
                                    <button type="button"
                                        className="btn btn-primary float-start"
                                        onClick={() => this.updateClick()}
                                    >Update</button>
                                    : null}

                            </div>

                        </div>
                    </div>
                </div>

            </div>
        )
    }
}