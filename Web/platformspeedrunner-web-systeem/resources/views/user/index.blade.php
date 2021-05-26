@extends('layouts.app')

@section('title')
    User
@endsection

@section('content')
{{--Content Header (Page header)--}}
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Users</h1>
                </div>
            </div>
        </div>
    </section>

{{--Main Content--}}
<section class="content">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <a class="btn btn-primary" style="float: right" href="{{ route('user.create') }}"><i class="fas fa-plus"></i> New user</a>
                    </div>

                    @if ($message = Session::get('success'))
                        <div class="alert alert-success">
                            <p>{{ $message }}</p>
                        </div>
                    @endif

                    <div class="card-body">
                        <div class="table-responsive">
                            <table id="usersTable" class="table table-bordered table-striped SpeedRunnerTable">
                                <thead>
                                <tr>
                                    <th class="default-order">#</th>
                                    <th>Username</th>
                                    <th>Unique Key</th>
                                    <th>Upvotes</th>
                                    <th>Role</th>
                                    <th>Deleted</th>
                                    <th class="no-order">Actions</th>
                                </tr>
                                </thead>
                                <tbody>
                                @foreach ($users as $user)
                                    <tr>
                                        <td>{{ $user->id }}</td>
                                        <td>{{ $user->username }}</td>
                                        <td>{{ $user->unique_key }}</td>
                                        <td>{{ $user->upvotes }}</td>
                                        <td>{{ (new App\Http\Controllers\RoleController)->GetName($user->role_id) }}</td>
                                        <td>
                                            @if ($user->active === 1)
                                                no
                                            @else
                                                yes
                                            @endif
                                        </td>
                                        <td>
                                            <form action="{{ route('user.destroy',$user->id) }}" method="POST">
                                            <a class="btn btn-sm btn-primary " href="{{ route('user.show',$user->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                            <a class="btn btn-sm btn-secondary" href="{{ route('user.edit',$user->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                @csrf
                                                @method('DELETE')
                                                <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this user?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                            </form>
                                        </td>
                                    </tr>
                                @endforeach
                                </tbody>
                                <tfoot>
                                <tr>
                                    <th>#</th>
                                    <th>Username</th>
                                    <th>Unique Key</th>
                                    <th>Upvotes</th>
                                    <th>Role</th>
                                    <th>Deleted</th>
                                    <th>Actions</th>
                                </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
@endsection
@section('pagejs')
    @include('inc.datatablefiltering')
@endsection
