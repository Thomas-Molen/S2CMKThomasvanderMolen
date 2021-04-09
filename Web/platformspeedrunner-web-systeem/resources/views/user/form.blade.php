<div class="box box-info padding-1">
    <div class="box-body">

        <div class="form-group">
            {{ Form::label('username') }}
            {{ Form::text('username', $user->username, ['class' => 'form-control' . ($errors->has('username') ? ' is-invalid' : ''), 'placeholder' => 'Username']) }}
            {!! $errors->first('username', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('password') }}
            {{ Form::text('password', $user->password, ['class' => 'form-control' . ($errors->has('password') ? ' is-invalid' : ''), 'placeholder' => 'Password']) }}
            {!! $errors->first('password', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('unique_key') }}
            {{ Form::text('unique_key', $user->unique_key, ['class' => 'form-control' . ($errors->has('unique_key') ? ' is-invalid' : ''), 'placeholder' => 'Unique Key']) }}
            {!! $errors->first('unique_key', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('upvotes') }}
            {{ Form::text('upvotes', $user->upvotes, ['class' => 'form-control' . ($errors->has('upvotes') ? ' is-invalid' : ''), 'placeholder' => 'Upvotes']) }}
            {!! $errors->first('upvotes', '<div class="invalid-feedback">:message</p>') !!}
        </div>


    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
