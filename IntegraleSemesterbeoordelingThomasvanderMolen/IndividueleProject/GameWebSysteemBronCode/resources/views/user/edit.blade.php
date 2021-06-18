@extends('layouts.app')

@section('title')
    Edit user: {{ $user->username }}
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Edit user: <strong>{{ $user->username }}</strong></h1>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content container-fluid">
        <div class="container-fluid">
            <div class="col-md-12">

                @includeif('partials.errors')

                <div class="card card-default">
                    <div class="card-body">
                        <form method="POST" action="{{ route('user.update', $user->id) }}"  role="form" enctype="multipart/form-data">
                            {{ method_field('PATCH') }}
                            @csrf

                            @include('user.form')

                        </form>
                    </div>
                </div>
            </div>
            <div style="display: inline-block">
                <a class="btn btn-secondary" href="javascript:history.go(-1)">Back</a>
            </div>
                <div style="display: inline-block">
                    <form action="{{ route('user.destroy',$user->id) }}" method="POST">

                        @csrf
                        @method('DELETE')

                        <button type="submit" class="btn btn-danger" onclick="return confirm('Are you sure you want to delete your account?');">Delete</button>
                    </form>
                </div>
        </div>
    </section>
@endsection
