@extends('layouts.app')

@section('title')
    Comment
@endsection

@section('content')
    {{--Content Header (Page header)--}}
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Links</h1>
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

                        <div class="card-body">
                            <div class="table-responsive">
                                <table id="commentsTable" class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th class="default-order">#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Name</th>
                                        <th>Url</th>
                                        <th class="no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ($links as $link)
                                        <tr>
                                            <td>{{ $link->id }}</td>
                                            <td>{{ $link->user->username }}</td>
                                            <td>{{ $link->run->custom_name }}</td>
                                            <td>{{ $link->name }}</td>
                                            <td><a href="{{ $link->url }}" target="_blank" rel="noopener noreferrer">{{ $readabilityHelper->ShortenString($link->url, 100)}}</a></td>
                                            <td>
                                                <form action="{{ route('link.destroy',$link->id) }}" method="POST">
                                                    <a class="btn btn-sm btn-primary " href="{{ route('link.show',$link->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                    <a class="btn btn-sm btn-secondary" href="{{ route('link.edit',$link->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                    @csrf
                                                    @method('DELETE')
                                                    <button type="submit" class="btn btn-danger btn-sm"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                </form>
                                            </td>
                                        </tr>
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Name</th>
                                        <th>Url</th>
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
