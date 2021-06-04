@extends('layouts.app')

@section('template_title')
    Update Link
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Edit link: <strong>{{ $link->name }}</strong></h1>
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
                        <form method="POST" action="{{ route('link.update', $link->id) }}"  role="form" enctype="multipart/form-data">
                            {{ method_field('PATCH') }}
                            @csrf

                            @include('link.form')

                        </form>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="javascript:history.go(-1)">Back</a>
            @if($link->id !== null)
                <div style="display: inline-block">
                    <form action="{{ route('link.destroy',$link->id) }}" method="POST">

                        @csrf
                        @method('DELETE')

                        <button type="submit" class="btn btn-danger" onclick="return confirm('Are you sure you want to delete this link?');">Delete</button>
                    </form>
                </div>
            @endif
        </div>
    </section>
@endsection
