@extends('layouts.app')

@section('template_title')
    Update Link
@endsection

@section('content')
    <section class="content container-fluid">
        <div class="">
            <div class="col-md-12">

                @includeif('partials.errors')

                <div class="card card-default">
                    <div class="card-header">
                        <span class="card-title">Update Link</span>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="{{ route('link.update', $link->id) }}"  role="form" enctype="multipart/form-data">
                            {{ method_field('PATCH') }}
                            @csrf

                            @include('link.form')
                        </form>
                    </div>
                </div>
                <div style="display: inline-block">
                    <a class="btn btn-secondary" href="{{ (new \App\Http\Helpers\RoutingHelper)->PreviousRoute() }}">Back</a>
                </div>
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
        </div>
    </section>
@endsection
