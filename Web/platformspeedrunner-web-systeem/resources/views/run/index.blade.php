@extends('layouts.app')

@section('title')
    Run
@endsection

@section('content')
    {{--Content Header (Page header)--}}
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Runs</h1>
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

                        @if ($message = Session::get('success'))
                            <div class="alert alert-success">
                                <p>{{ $message }}</p>
                            </div>
                        @endif
                        @if ($message = Session::get('error'))
                            <div class="alert alert-danger">
                                <p>{{ $message }}</p>
                            </div>
                        @endif

                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th class="default-order">#</th>
                                        <th>Name</th>
                                        <th>Player</th>
                                        <th>Time</th>
                                        <th>Date</th>
                                        <th>Deleted</th>
                                        <th class="no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ($runs as $run)
                                            <tr>
                                                <td>{{ $run->id }}</td>
                                                <td>{{ $run->custom_name }}</td>
                                                <td>{{ $run->user->username }}</td>
                                                <td>{{ $readabilityHelper->FormatTime($run->duration) }}</td>
                                                <td>{{ $run->created_at . " (UTC)"}}</td>
                                                <td>
                                                    @if ($run->active === 1)
                                                        no
                                                    @else
                                                        yes
                                                    @endif
                                                </td>
                                                <td>
                                                    <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                        @csrf
                                                        @method('DELETE')
                                                        <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                    </form>
                                                </td>
                                            </tr>
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Player</th>
                                        <th>Time</th>
                                        <th>Date</th>
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
